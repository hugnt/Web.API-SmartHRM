using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
	public class DashboardController : Controller
	{
        [Route("/Hoang/Dashboard")]
        public IActionResult HoangIndex()
        {
            return View();
        }

    }
}

