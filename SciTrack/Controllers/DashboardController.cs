using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IHttpClientFactory httpClientFactory, ILogger<DashboardController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Trang Dashboard chính
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// API lấy dữ liệu thống kê cho Dashboard
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Lấy dữ liệu song song để tăng hiệu suất
                var taiSanTask = client.GetAsync("/api/TaiSans");
                var deTaiTask = client.GetAsync("/api/DeTais");
                var ketQuaTask = client.GetAsync("/api/KetQuaDeTai");
                var hopDongTask = client.GetAsync("/api/HopDong");
                var thietBiTask = client.GetAsync("/api/TBKHCN");

                await Task.WhenAll(taiSanTask, deTaiTask, ketQuaTask, hopDongTask, thietBiTask);

                // Parse dữ liệu
                var taiSanList = await ParseResponseAsync<TaiSanDto>(taiSanTask.Result, options);
                var deTaiList = await ParseResponseAsync<DeTaiDto>(deTaiTask.Result, options);
                var ketQuaList = await ParseResponseAsync<KetQuaDto>(ketQuaTask.Result, options);
                var hopDongList = await ParseResponseAsync<HopDongDto>(hopDongTask.Result, options);
                var thietBiList = await ParseResponseAsync<ThietBiDto>(thietBiTask.Result, options);

                // 1. Thống kê tổng quan
                var summary = new
                {
                    totalTaiSan = taiSanList.Count,
                    totalDeTai = deTaiList.Count,
                    totalKetQua = ketQuaList.Count,
                    totalHopDong = hopDongList.Count
                };

                // 2. Kinh phí đề tài (lấy top 10)
                var topDeTai = deTaiList
                    .Where(d => d.KinhPhiThucHien.HasValue && d.KinhPhiThucHien > 0)
                    .OrderByDescending(d => d.KinhPhiThucHien ?? 0)
                    .Take(10)
                    .ToList();

                var kinhPhiDeTai = new
                {
                    labels = topDeTai.Select(d => d.MaDeTai ?? $"DT{d.Id}").ToList(),
                    data = topDeTai.Select(d => d.KinhPhiThucHien ?? 0).ToList()
                };

                // 3. Phân loại kết quả đề tài
                var phanLoaiGroups = ketQuaList
                    .GroupBy(k => string.IsNullOrEmpty(k.PhanLoai) ? "Chưa phân loại" : k.PhanLoai)
                    .Select(g => new { Label = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var phanLoaiKetQua = new
                {
                    labels = phanLoaiGroups.Select(p => p.Label).ToList(),
                    data = phanLoaiGroups.Select(p => p.Count).ToList()
                };

                // 4. Giá trị hợp đồng theo đối tác (top 10)
                var topHopDong = hopDongList
                    .Where(h => h.TongGiaTriHopDong.HasValue && h.TongGiaTriHopDong > 0)
                    .OrderByDescending(h => h.TongGiaTriHopDong ?? 0)
                    .Take(10)
                    .ToList();

                var hopDongGiaTri = new
                {
                    labels = topHopDong.Select(h => TruncateString(h.TenDoiTac ?? "N/A", 20)).ToList(),
                    data = topHopDong.Select(h => h.TongGiaTriHopDong ?? 0).ToList()
                };

                // 5. Tình trạng thiết bị
                var tinhTrangGroups = thietBiList
                    .GroupBy(t => string.IsNullOrEmpty(t.TinhTrangThietBi) ? "Chưa cập nhật" : t.TinhTrangThietBi)
                    .Select(g => new { Label = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var tinhTrangThietBi = new
                {
                    labels = tinhTrangGroups.Select(t => t.Label).ToList(),
                    data = tinhTrangGroups.Select(t => t.Count).ToList()
                };

                _logger.LogInformation("Dashboard data loaded: {TaiSan} tài sản, {DeTai} đề tài, {KetQua} kết quả, {HopDong} hợp đồng, {ThietBi} thiết bị",
                    taiSanList.Count, deTaiList.Count, ketQuaList.Count, hopDongList.Count, thietBiList.Count);

                return Json(new
                {
                    success = true,
                    summary,
                    kinhPhiDeTai,
                    phanLoaiKetQua,
                    hopDongGiaTri,
                    tinhTrangThietBi
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                return Json(new
                {
                    success = false,
                    message = $"Lỗi kết nối API: {ex.Message}"
                });
            }
        }

        private async Task<List<T>> ParseResponseAsync<T>(HttpResponseMessage response, JsonSerializerOptions options)
        {
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            }
            return new List<T>();
        }

        private string TruncateString(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        #region DTOs for Dashboard

        private class TaiSanDto
        {
            public int Id { get; set; }
            public string? SoDanhMuc { get; set; }
            public string? Ten { get; set; }
            public decimal? NguyenGia { get; set; }
            public decimal? GiaTriConLai { get; set; }
            public string? TrangThaiTaiSan { get; set; }
        }

        private class DeTaiDto
        {
            public int Id { get; set; }
            public string? MaDeTai { get; set; }
            public string? Ten { get; set; }
            public decimal? KinhPhiThucHien { get; set; }
        }

        private class KetQuaDto
        {
            public int Id { get; set; }
            public string? MaKetQua { get; set; }
            public string? TenKetQua { get; set; }
            public string? PhanLoai { get; set; }
        }

        private class HopDongDto
        {
            public int Id { get; set; }
            public string? MaHopDong { get; set; }
            public string? TenDoiTac { get; set; }
            public decimal? TongGiaTriHopDong { get; set; }
        }

        private class ThietBiDto
        {
            public int Id { get; set; }
            public string? MaThietBi { get; set; }
            public string? TenThietBi { get; set; }
            public string? TinhTrangThietBi { get; set; }
        }

        #endregion
    }
}
