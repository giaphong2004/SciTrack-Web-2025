using Microsoft.AspNetCore.Mvc;

namespace SciTrack.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TSKHCNController : Controller
    {
        private static readonly List<object> _data = new() {
        new { id = 1, maSo = "TS001", ten = "Máy in HP", nguyenGia = 5000000m, trangThai="Đang sử dụng" }
    };

        [HttpGet] public IActionResult Get() => Ok(_data);
    }
}
