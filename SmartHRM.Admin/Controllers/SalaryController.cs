﻿using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
    public class SalaryController : Controller
    {

        [Route("Salary/AllowanceList")]
        public IActionResult Allowance()
        {
            return View("~/Views/Salary/Allowance.cshtml");
        }
        [Route("Salary/AllowanceList/Trash")]
        public IActionResult AllowanceTrash()
        {
            return View();
        }
        [Route("Salary/BonusList")]
        public IActionResult Bonus()
        {
            return View();
        }
        [Route("Salary/BonusList/Trash")]
        public IActionResult BonusTrash()
        {
            return View();
        }
    }
}