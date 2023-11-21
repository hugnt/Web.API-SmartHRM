using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class DasboardController : Controller
    {
        [Route("Su-Dashboard")]
        public IActionResult SuIndex()
        {
            return View("~/Views/Dasboard/SuIndex.cshtml");
        }

        [Route("Hoang-Dashboard")]
        public IActionResult HoangIndex()
        {
            return View("~/Views/Dasboard/HoangIndex.cshtml");
        }

        [Route("Khanh-Dashboard")]
        public IActionResult KhanhIndex()
        {
            return View("~/Views/Dasboard/KhanhIndex.cshtml");
        }
    }
}
