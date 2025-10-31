using Microsoft.AspNetCore.Mvc;

namespace SciTrack.web.Controllers
{
    public class KetQuaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
