using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class InsuranceController : Controller
    {
        [Route("Insurance/InsuranceList")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
