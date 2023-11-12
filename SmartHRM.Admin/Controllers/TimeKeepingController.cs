using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class TimeKeepingController : Controller
    {
        [Route("TimeKeeping")]
        [Route("TimeKeeping/timeKeeper")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
