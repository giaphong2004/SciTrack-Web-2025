using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;
using System.Text.Json;

namespace SciTrack.web.Controllers
{
    public class TaiSanController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TaiSanController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Tạo HttpClient với base URL từ appsettings
                var httpClient = _httpClientFactory.CreateClient("api");
                
                // Gọi API GET /api/TaiSans
                var response = await httpClient.GetAsync("/api/TaiSans");

                if (response.IsSuccessStatusCode)
                {
                    // Đọc JSON response
                    var json = await response.Content.ReadAsStringAsync();
                    
                    // Deserialize JSON thành List<TaiSan>
                    var taiSans = JsonSerializer.Deserialize<List<TaiSan>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return View(taiSans ?? new List<TaiSan>());
                }
                else
                {
                    ViewBag.Error = $"Lỗi API: {response.StatusCode} - {response.ReasonPhrase}";
                    return View(new List<TaiSan>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Lỗi kết nối API: {ex.Message}";
                return View(new List<TaiSan>());
            }
        }
    }
}
