using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SciTrack.web.Models;
using System.Text;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class DeTaiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DeTaiController> _logger;

        public DeTaiController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DeTaiController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? maDeTai)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

 
                var response = await httpClient.GetAsync("/api/DeTais");
                var list = new List<DeTai>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<DeTai>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DeTai>();
                }

              
                var ketQuaResponse = await httpClient.GetAsync("/api/KetQuaDeTai");
                var ketQuaList = new List<KetQua>();
                
                if (ketQuaResponse.IsSuccessStatusCode)
                {
                    var ketQuaJson = await ketQuaResponse.Content.ReadAsStringAsync();
                    ketQuaList = JsonSerializer.Deserialize<List<KetQua>>(ketQuaJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<KetQua>();
                }

                DeTai? selected = null;
                if (!string.IsNullOrEmpty(maDeTai))
                {
                    selected = list.FirstOrDefault(x => x.MaDeTai == maDeTai);
                }

                ViewBag.Selected = selected;
                ViewBag.KetQuaList = ketQuaList; 
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading DeTai index");
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                ViewBag.KetQuaList = new List<KetQua>();
                return View(new List<DeTai>());
            }
        }

        // 🟩 Tạo mới
        [HttpPost]
        public async Task<IActionResult> Create(DeTai model)
        {
            try
            {
               
                var dto = new
                {
                    maSoDeTai = model.MaDeTai,
                    ten = model.Ten,
                    ngayCapNhatTaiSan = model.CapNhatTaiSanLanCuoi,
                    cacQuyetDinhLienQuan = model.QuyetDinhThamChieu,
                    kinhPhiThucHien = model.KinhPhiThucHien,
                    kinhPhiGiaoKhoaChuyen = model.KinhPhiDaoTao,
                    kinhPhiVatTuTieuHao = model.KinhPhiTieuHao,
                    haoMonKhauHaoLienQuan = model.KhauHaoThietBi,
                    quyetDinhXuLyTaiSan = model.QuyetDinhXuLyTaiSan,
                    ketQuaDeTai = model.KetQuaDeTaiId
                };

                _logger.LogInformation("Creating DeTai with MaDeTai={MaDeTai}, Ten={Ten}, KetQuaDeTaiId={KetQuaDeTaiId}", 
                    model.MaDeTai, model.Ten, model.KetQuaDeTaiId);

                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Sending to API: {Json}", json);

                var response = await client.PostAsync("/api/DeTais", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✅ Thêm đề tài thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Create DeTai failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                    
                 
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể thêm đề tài!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể thêm đề tài! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating DeTai");
                TempData["Message"] = $"❌ Lỗi thêm mới: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // 🟨 Cập nhật
        [HttpPost]
        public async Task<IActionResult> Update(DeTai model)
        {
            try
            {
              
                var dto = new
                {
                    ten = model.Ten,
                    ngayCapNhatTaiSan = model.CapNhatTaiSanLanCuoi,
                    cacQuyetDinhLienQuan = model.QuyetDinhThamChieu,
                    kinhPhiThucHien = model.KinhPhiThucHien,
                    kinhPhiGiaoKhoaChuyen = model.KinhPhiDaoTao,
                    kinhPhiVatTuTieuHao = model.KinhPhiTieuHao,
                    haoMonKhauHaoLienQuan = model.KhauHaoThietBi,
                    quyetDinhXuLyTaiSan = model.QuyetDinhXuLyTaiSan,
                    ketQuaDeTai = model.KetQuaDeTaiId
                };

                _logger.LogInformation("Updating DeTai ID {Id}: KetQuaDeTaiId={KetQuaDeTaiId}", 
                    model.Id, model.KetQuaDeTaiId);

                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

              
                var response = await client.PutAsync($"/api/DeTais/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✏️ Cập nhật đề tài thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Update DeTai failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                    
                  
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể cập nhật đề tài!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể cập nhật đề tài! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating DeTai");
                TempData["Message"] = $"❌ Lỗi cập nhật: {ex.Message}";
            }

            return RedirectToAction("Index", new { maDeTai = model.MaDeTai });
        }

        // 🟥 Xóa
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting DeTai ID: {Id}", id);

                var client = _httpClientFactory.CreateClient("api");
                var response = await client.DeleteAsync($"/api/DeTais/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = $"🗑️ Đã xóa đề tài thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Delete DeTai failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                    
                  
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể xóa đề tài!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể xóa đề tài: {errorContent}";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting DeTai");
                TempData["Message"] = $"❌ Lỗi khi xóa: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
