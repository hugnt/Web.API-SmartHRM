using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class TimeKeepingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("TimeKeeping")]
        [Route("TimeKeeping/TimeKeeper")]
        public IActionResult TimeKeeper()
        {
            return View();
        }

        [Route("TimeKeeping")]
        [Route("TimeKeeping/TimeKeeper/Trash")]
        public IActionResult TimeKeeperTrash()
        {
            return View();
        }
    }
}
