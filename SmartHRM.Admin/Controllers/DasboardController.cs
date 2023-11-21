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
    }
}
