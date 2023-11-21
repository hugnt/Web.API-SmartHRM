using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Models;
using System.Diagnostics;

namespace SmartHRM.Admin.Controllers
{
    public class KhanhController : Controller
    {
        private readonly ILogger<KhanhController> _logger;

        public KhanhController(ILogger<KhanhController> logger)
        {
            _logger = logger;
        }
        [Route("/Dashboard")]
        [Route("")]
        public IActionResult KhanhIndex()
        {
            return View("~/Views/Home/KhanhIndex.cshtml");
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