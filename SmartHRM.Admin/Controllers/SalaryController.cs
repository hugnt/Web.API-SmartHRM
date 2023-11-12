using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class SalaryController : Controller
    {

        [Route("Salary")]
        [Route("Salary/DeductionList")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
