using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;
using System.Text;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class ThietBiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ThietBiController> _logger;

        public ThietBiController(IHttpClientFactory httpClientFactory, ILogger<ThietBiController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

         
                var response = await httpClient.GetAsync("/api/TBKHCN");
                var list = new List<ThietBi>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<ThietBi>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ThietBi>();
                }

             
                ThietBi? selected = null;
                if (id.HasValue)
                {
                    selected = list.FirstOrDefault(x => x.Id == id.Value);
                }

                ViewBag.Selected = selected;
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ThietBi index");
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                return View(new List<ThietBi>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThietBi model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/TBKHCN", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✅ Thêm thiết bị thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể thêm thiết bị!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể thêm thiết bị! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ThietBi");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ThietBi model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/api/TBKHCN/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✏️ Cập nhật thiết bị thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể cập nhật thiết bị!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể cập nhật thiết bị! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ThietBi");
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
                var response = await client.DeleteAsync($"/api/TBKHCN/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "🗑️ Xóa thiết bị thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể xóa thiết bị!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể xóa thiết bị! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting ThietBi");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
