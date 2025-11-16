using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;
using System.Text;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class TaiSanController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TaiSanController> _logger;

        public TaiSanController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TaiSanController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("api");

            
                var response = await httpClient.GetAsync("/api/TaiSans");
                var list = new List<TaiSan>();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("API TaiSans response: {Json}", json);
                    
                    list = JsonSerializer.Deserialize<List<TaiSan>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TaiSan>();
                    
                    _logger.LogInformation("Deserialized {Count} tai san records", list.Count);
                }
                else
                {
                    _logger.LogWarning("API TaiSans returned status: {StatusCode}", response.StatusCode);
                }

            
                var deTaiResponse = await httpClient.GetAsync("/api/DeTais");
                var deTaiList = new List<DeTai>();
                
                if (deTaiResponse.IsSuccessStatusCode)
                {
                    var deTaiJson = await deTaiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("API DeTais response: {Json}", deTaiJson);
                    
                    deTaiList = JsonSerializer.Deserialize<List<DeTai>>(deTaiJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DeTai>();
                    
                    _logger.LogInformation("Deserialized {Count} de tai records", deTaiList.Count);
                }
                else
                {
                    _logger.LogWarning("API DeTais returned status: {StatusCode}", deTaiResponse.StatusCode);
                }

               
                TaiSan? selected = null;
                if (id.HasValue)
                {
                    var resDetail = await httpClient.GetAsync($"/api/TaiSans/{id.Value}");
                    if (resDetail.IsSuccessStatusCode)
                    {
                        var jsonDetail = await resDetail.Content.ReadAsStringAsync();
                        selected = JsonSerializer.Deserialize<TaiSan>(jsonDetail,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }

             
                ViewBag.Selected = selected;
                ViewBag.DeTaiList = deTaiList;
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading TaiSan index");
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                ViewBag.DeTaiList = new List<DeTai>();
                return View(new List<TaiSan>());
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(TaiSan model)
        {
            try
            {
             
                if (model.MaDeTaiKHCN == 0)
                {
                    model.MaDeTaiKHCN = null;
                }
                
                _logger.LogInformation("Creating TaiSan: MaDeTaiKHCN={MaDeTaiKHCN}, Ten={Ten}", 
                    model.MaDeTaiKHCN, model.Ten);
                
                var httpClient = _httpClientFactory.CreateClient("api");

                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Sending to API: {Json}", json);
                
                var response = await httpClient.PostAsync("/api/TaiSans", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✅ Thêm mới tài sản thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Create TaiSan failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                    
                   
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Lỗi khi thêm mới tài sản!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Lỗi khi thêm mới tài sản! ({response.StatusCode})";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating TaiSan");
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
                var response = await httpClient.DeleteAsync($"/api/TaiSans/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "🗑️ Xóa tài sản thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Delete TaiSan failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                    
                  
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Lỗi xóa: {response.StatusCode}";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Lỗi xóa: {response.StatusCode}";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting TaiSan");
                TempData["Message"] = $"❌ Lỗi khi gọi API: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(TaiSan model)
        {
            try
            {
              
                if (model.MaDeTaiKHCN == 0)
                {
                    model.MaDeTaiKHCN = null;
                }
                
                _logger.LogInformation("Updating TaiSan ID {Id}: MaDeTaiKHCN={MaDeTaiKHCN}, Ten={Ten}", 
                    model.Id, model.MaDeTaiKHCN, model.Ten);
                
                var client = _httpClientFactory.CreateClient("api");
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("Sending to API: {Json}", json);
                
                var response = await client.PutAsync($"api/TaiSans/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "✏️ Cập nhật tài sản thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Update TaiSan failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                    
                  
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                        if (errorObj.TryGetProperty("message", out var messageElement))
                        {
                            TempData["Message"] = $"⚠️ {messageElement.GetString()}";
                        }
                        else
                        {
                            TempData["Message"] = $"⚠️ Không thể cập nhật tài sản!";
                        }
                    }
                    catch
                    {
                        TempData["Message"] = $"⚠️ Không thể cập nhật tài sản! ({response.StatusCode})";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating TaiSan");
                TempData["Message"] = $"❌ Lỗi cập nhật: {ex.Message}";
            }

            return RedirectToAction("Index", new { id = model.Id });
        }

    }
}

