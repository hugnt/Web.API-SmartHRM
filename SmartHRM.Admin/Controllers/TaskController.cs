﻿using Microsoft.AspNetCore.Mvc;

namespace SmartHRM.Admin.Controllers
{
	public class TaskController : Controller
	{
        [Route("Tasks/Project")]
        public IActionResult Index()
		{
			return View();
		}

        [Route("Tasks/Project/Trash")]
        public IActionResult ProjectTrash()
        {
            return View();
        }

        [Route("Tasks/TaskList")]
        public IActionResult Task()
        {
            return View();
        }

        [Route("Tasks/TaskList/Trash")]
        public IActionResult TaskTrash()
        {
            return View();
        }

    }
}

