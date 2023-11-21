using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Models;
using System.Diagnostics;

namespace SmartHRM.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("/Dashboard/Su")]
        [Route("")]
        public IActionResult SuIndex()
        {
            return View("~/Views/Home/SuIndex.cshtml");
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