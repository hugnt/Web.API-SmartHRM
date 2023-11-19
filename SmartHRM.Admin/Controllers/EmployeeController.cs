using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class EmployeeController : Controller
    {

        [Route("Employee")]
        [Route("Employee/PersonnelFiles")]
        public IActionResult PersonnelFiles()
        {
            return View("~/Views/Employee/PersonnelFiles.cshtml");
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
     
        [Route("Employee/Contract/Trash")]
        public IActionResult ContractTrash()
        {
            return View();
        }
        [Route("Employee/Department/Trash")]
        public IActionResult DepartmentTrash()
        {
            return View();
        }
    }
}
