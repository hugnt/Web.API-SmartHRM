using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class SalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Salary")]
        [Route("Salary/DeductionList")]
        public IActionResult DeductionList()
        {
            return View();
        }
        [Route("Salary")]
        [Route("Salary/DeductionList/Trash")]
        public IActionResult DeductionListTrash() 
        { 
            return View(); 
        }
    }

}
