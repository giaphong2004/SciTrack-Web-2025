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
             
            };
            return View(list);
        }
    }
}
