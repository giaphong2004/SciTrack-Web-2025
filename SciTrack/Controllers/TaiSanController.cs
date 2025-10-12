using Microsoft.AspNetCore.Mvc;

namespace SciTrack.web.Controllers
{
    public class TaiSanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
