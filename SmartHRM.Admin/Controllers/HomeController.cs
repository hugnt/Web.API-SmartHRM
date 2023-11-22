using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Attributes;
using SmartHRM.Admin.Models;
using System.Diagnostics;

namespace SmartHRM.Admin.Controllers
{
    [CustomAuthorize("Admin", "Employee")]
    public class HomeController : Controller
    {
        
        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}