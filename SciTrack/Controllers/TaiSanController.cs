using Microsoft.AspNetCore.Mvc;
using SciTrack.web.Models;

namespace SciTrack.web.Controllers
{
    public class TaiSanController : Controller
    {
        public IActionResult Index()
        {
            var list = new List<TaiSan>
            {
                new TaiSan { MaTaiSan = "TS001", TenTaiSan = "Máy tính Dell", TrangThai = "Đang sử dụng" },
                new TaiSan { MaTaiSan = "TS002", TenTaiSan = "Máy in Canon", TrangThai = "Bảo trì" },
                new TaiSan { MaTaiSan = "TS003", TenTaiSan = "Bộ đo nhiệt độ", TrangThai = "Tốt" }
            };
            return View(list);
        }
    }
}
