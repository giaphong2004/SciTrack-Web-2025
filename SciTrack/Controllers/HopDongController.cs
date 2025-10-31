using Microsoft.AspNetCore.Mvc;

namespace SciTrack.web.Controllers
{
    public class HopDongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
