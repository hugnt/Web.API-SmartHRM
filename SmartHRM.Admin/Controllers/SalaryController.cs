using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class SalaryController : Controller
    {

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
