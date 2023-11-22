using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Attributes;

namespace SmartHRM.Admin.Controllers
{
    public class SalaryController : Controller
    {
        [CustomAuthorize("Admin")]
        [Route("Salary/SalaryOfEmployee")]
        public IActionResult SalaryOfEmployee()
        {
            return View("~/Views/Salary/SalaryOfEmployee.cshtml");
        }
        [Route("Salary/AllowanceList")]
        public IActionResult Allowance()
        {
            return View("~/Views/Salary/Allowance.cshtml");
        }
        [Route("Salary/AllowanceList/Trash")]
        public IActionResult AllowanceTrash()
        {
            return View();
        }
        [Route("Salary/BonusList")]
        public IActionResult Bonus()
        {
            return View();
        }
        [Route("Salary/BonusList/Trash")]
        public IActionResult BonusTrash()
        { 
            return View(); 
        }

        [Route("Salary")]
        [Route("Salary/DeductionList")]
        public IActionResult DeductionList()
        {
            return View("~/Views/Salary/DeductionList.cshtml");
        }
        [Route("Salary")]
        [Route("Salary/DeductionList/Trash")]
        public IActionResult DeductionListTrash()
        {
            return View();
        }
    }

}
