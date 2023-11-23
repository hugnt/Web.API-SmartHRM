using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class InsuranceController : Controller
    {

        [Route("Insurance")]
        [Route("Insurance/InsuranceList")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Insurance")]
        [Route("Insurance/InsuranceList/Trash")]
        public IActionResult InsuranceTrash()
        {
            return View();
        }

        [Route("Insurance/InsuranceOfEmployee")]
        public IActionResult InsuranceOfEmployee()
        {
            return View();
        }

    }
}
