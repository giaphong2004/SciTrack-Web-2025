using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;
using System.Text;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class TBKHCNController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TBKHCNController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

                // 🟦 Lấy toàn bộ danh sách
                var response = await httpClient.GetAsync("/api/TBKHCN");
                var list = new List<Models.TBKHCN>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<Models.TBKHCN>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Models.TBKHCN>();
                }

                // 🟨 Nếu có id => Gọi API /api/TaiSans/{id} để lấy chi tiết
                Models.TBKHCN? selected = null;
                if (id.HasValue)
                {
                    var resDetail = await httpClient.GetAsync($"/api/TBKHCN/{id.Value}");
                    if (resDetail.IsSuccessStatusCode)
                    {
                        var jsonDetail = await resDetail.Content.ReadAsStringAsync();
                        selected = JsonSerializer.Deserialize<Models.TBKHCN>(jsonDetail,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }

                // 🟩 Truyền vào ViewBag để form bên trái hiển thị
                ViewBag.Selected = selected;
                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                return base.View(new List<Models.TBKHCN>());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Models.TBKHCN model)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/api/TBKHCN", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✅ Thêm mới tài sản thành công!";
                }
                else
                {
                    TempData["Message"] = "⚠️ Lỗi khi thêm mới tài sản!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ Lỗi kết nối API: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");
                var response = await httpClient.DeleteAsync($"/api/TBKHCN/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "🗑️ Xóa tài sản thành công!";
                }
                else
                {
                    TempData["Message"] = $"⚠️ Lỗi xóa: {response.StatusCode}";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ Lỗi khi gọi API: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(Models.TBKHCN model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/TBKHCN/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✏️ Cập nhật tài sản thành công!";
                }
                else
                {
                    TempData["Message"] = $"⚠️ Không thể cập nhật tài sản! ({response.StatusCode})";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"❌ Lỗi cập nhật: {ex.Message}";
            }

            return RedirectToAction("Index", new { id = model.Id });
        }

    }
}

