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

        public DeTaiController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // 🟦 Lấy danh sách đề tài + chi tiết nếu có id
        public async Task<IActionResult> Index(string? maDeTai)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

                // 🟩 Lấy toàn bộ danh sách đề tài
                var response = await httpClient.GetAsync("/api/DeTais");
                var list = new List<DeTai>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<DeTai>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DeTai>();
                }

                // 🟨 Nếu có mã đề tài được chọn => gọi API chi tiết hoặc lấy từ list
                DeTai? selected = null;
                if (!string.IsNullOrEmpty(maDeTai))
                {
                    selected = list.FirstOrDefault(x => x.MaDeTai == maDeTai);
                }

                ViewBag.Selected = selected;
                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                return View(new List<DeTai>());
            }
        }

        // 🟩 Tạo mới
        [HttpPost]
        public async Task<IActionResult> Create(DeTai model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/DeTais", content);

                TempData["Message"] = response.IsSuccessStatusCode
                    ? "✅ Thêm đề tài thành công!"
                    : $"⚠️ Không thể thêm đề tài! ({response.StatusCode})";
            }
            catch (Exception ex)
            {
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
                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/DeTais/{model.MaDeTai}", content);

                TempData["Message"] = response.IsSuccessStatusCode
                    ? "✏️ Cập nhật đề tài thành công!"
                    : $"⚠️ Không thể cập nhật đề tài! ({response.StatusCode})";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ Lỗi cập nhật: {ex.Message}";
            }

            return RedirectToAction("Index", new { id = model.MaDeTai });
        }

        // 🟥 Xóa
        [HttpPost]
        public async Task<IActionResult> Delete(string maDeTai)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");
                var response = await client.DeleteAsync($"/api/DeTais/{maDeTai}");

                TempData["Message"] = response.IsSuccessStatusCode
                    ? $"🗑️ Đã xóa đề tài có mã {maDeTai} thành công!"
                    : $"⚠️ Không thể xóa đề tài! ({response.StatusCode})";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ Lỗi khi xóa: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
