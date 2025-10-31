using Microsoft.AspNetCore.Mvc;

namespace SciTrack.web.Controllers
{
    public class ThietBiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
