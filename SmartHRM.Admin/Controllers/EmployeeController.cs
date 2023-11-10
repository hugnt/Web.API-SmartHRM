using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class EmployeeController : Controller
    {

        [Route("Employee")]
        [Route("Employee/PersonnelFiles")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Employee/Department")]
        public IActionResult Department()
        {
            return View();
        }
        [Route("Employee/Contract")]
        public IActionResult Contract()
        {
            return View();
        }
    }
}
