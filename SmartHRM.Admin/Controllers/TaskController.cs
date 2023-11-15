using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
	public class TaskController : Controller
	{
        [Route("Tasks/Project")]
        public IActionResult Index()
		{
			return View();
		}
	}
}

