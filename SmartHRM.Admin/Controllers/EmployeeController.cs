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

        [Route("Employee/PersonnelFiles/Trash")]
        public IActionResult PersonalFileTrash()
        {
            return View();
        }

        [Route("Employee/Position")]
        public IActionResult Position()
        {
            return View();
        }

        [Route("Employee/Position/Trash")]
        public IActionResult PositionTrash()
        {
            return View();
        }
    }
}
