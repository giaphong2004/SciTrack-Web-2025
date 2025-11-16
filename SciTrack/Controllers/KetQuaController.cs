using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;
using System.Text;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class KetQuaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<KetQuaController> _logger;

        public KetQuaController(IHttpClientFactory httpClientFactory, ILogger<KetQuaController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

              
                var response = await httpClient.GetAsync("/api/KetQuaDeTai");
                var list = new List<KetQua>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonSerializer.Deserialize<List<KetQua>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<KetQua>();
                }

                
                var hopDongResponse = await httpClient.GetAsync("/api/HopDong");
                var hopDongList = new List<HopDong>();

                if (hopDongResponse.IsSuccessStatusCode)
                {
                    var hopDongJson = await hopDongResponse.Content.ReadAsStringAsync();
                    hopDongList = JsonSerializer.Deserialize<List<HopDong>>(hopDongJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<HopDong>();
                }

                
                KetQua? selected = null;
                if (id.HasValue)
                {
                    selected = list.FirstOrDefault(x => x.Id == id.Value);
                }

                ViewBag.Selected = selected;
                ViewBag.HopDongList = hopDongList;
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading KetQua index");
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                ViewBag.HopDongList = new List<HopDong>();
                return View(new List<KetQua>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(KetQua model, List<int>? HopDongIds)
        {
            try
            {
              
                model.HopDongIds = HopDongIds;

                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Creating KetQua with {Count} hop dong", HopDongIds?.Count ?? 0);

                var response = await client.PostAsync("/api/KetQuaDeTai", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✅ Thêm kết quả đề tài thành công!";
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
                            TempData["Message"] = $"⚠️ Không thể thêm kết quả!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể thêm kết quả! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating KetQua");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(KetQua model, List<int>? HopDongIds)
        {
            try
            {
              
                model.HopDongIds = HopDongIds;

                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Updating KetQua ID {Id} with {Count} hop dong", model.Id, HopDongIds?.Count ?? 0);

                var response = await client.PutAsync($"/api/KetQuaDeTai/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✏️ Cập nhật kết quả đề tài thành công!";
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
                            TempData["Message"] = $"⚠️ Không thể cập nhật kết quả!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể cập nhật kết quả! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating KetQua");
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
                var response = await client.DeleteAsync($"/api/KetQuaDeTai/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "🗑️ Xóa kết quả đề tài thành công!";
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
                            TempData["Message"] = $"⚠️ Không thể xóa kết quả!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể xóa kết quả! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting KetQua");
                TempData["Message"] = $"❌ Lỗi: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
