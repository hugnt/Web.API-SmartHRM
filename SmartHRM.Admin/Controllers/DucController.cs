using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Models;
using System.Diagnostics;

namespace SmartHRM.Admin.Controllers
{
    public class DucController : Controller
    {
        [Route("/Dashboard/Duc")]
        [Route("")]
        public IActionResult Duc()
        {
            return View("~/Views/Duc/Duc.cshtml");
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