﻿using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class AllControllercs : Controller
    {
        [Route("/Error")]
        public IActionResult Error()
        {
            return View();
        }

        [Route("/NotPermission")]
        public IActionResult NotPermission()
        {
            return View();
        }
    }
}
