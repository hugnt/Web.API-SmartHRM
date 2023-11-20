using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class InsuranceController : Controller
    {
        [Route("/Insurance/InsuranceOfEmployee")]
        public IActionResult InsuranceOfEmployee()
        {
            return View("~/Views/Insurance/InsuranceOfEmployee.cshtml");
        }
    }
}
