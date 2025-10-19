using Microsoft.AspNetCore.Mvc;

namespace SciTrack.web.Controllers
{
    public class DeTaiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
