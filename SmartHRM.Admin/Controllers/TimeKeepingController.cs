using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class TimeKeepingController : Controller
    {
        [Route("TimeKeeping")]
        [Route("TimeKeeping/TimeKeeper")]
        public IActionResult TimeKeeper()
        {
            return View("~/Views/TimeKeeping/TimeKeeper.cshtml");
        }

        [Route("TimeKeeping")]
        [Route("TimeKeeping/TimeKeeper/Trash")]
        public IActionResult TimeKeeperTrash()
        {
            return View();
        }
    }
}
