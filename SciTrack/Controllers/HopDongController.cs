using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;
using System.Text;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class HopDongController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HopDongController> _logger;

        public HopDongController(IHttpClientFactory httpClientFactory, ILogger<HopDongController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");

                // Lấy danh sách hợp đồng
                var response = await client.GetAsync("/api/HopDong");
                var list = new List<HopDong>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<HopDong>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<HopDong>();
                }

                // Lấy chi tiết một hợp đồng nếu có id (để sửa)
                HopDong? selected = null;
                if (id.HasValue)
                {
                    selected = list.FirstOrDefault(x => x.Id == id.Value);
                }

                ViewBag.Selected = selected;

                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading HopDong index");
                TempData["Message"] = $"❌ Lỗi tải dữ liệu: {ex.Message}";
                return View(new List<HopDong>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(HopDong model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");

                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Creating HopDong: {MaHopDong} - {TenDoiTac}", model.MaHopDong, model.TenDoiTac);

                var response = await client.PostAsync("/api/HopDong", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✅ Thêm hợp đồng thành công!";
                }
                else
                {
                    await HandleApiError(response, "Không thể thêm hợp đồng!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating HopDong");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(HopDong model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");

                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Updating HopDong ID {Id}", model.Id);

                var response = await client.PutAsync($"/api/HopDong/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✏️ Cập nhật hợp đồng thành công!";
                }
                else
                {
                    await HandleApiError(response, "Không thể cập nhật hợp đồng!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating HopDong");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");

                var response = await client.DeleteAsync($"/api/HopDong/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "🗑️ Xóa hợp đồng thành công!";
                }
                else
                {
                    await HandleApiError(response, "Không thể xóa hợp đồng!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting HopDong");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // ----------- HÀM DÙNG CHUNG ĐỂ XỬ LÝ LỖI API --------------
        private async Task HandleApiError(HttpResponseMessage response, string defaultMessage)
        {
            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var errorObj = JsonSerializer.Deserialize<JsonElement>(json);

                if (errorObj.TryGetProperty("message", out var msg))
                {
                    TempData["Message"] = $"⚠️ {msg.GetString()}";
                    return;
                }
            }
            catch { }

            TempData["Message"] = $"⚠️ {defaultMessage} ({response.StatusCode})";
        }
    }
}
