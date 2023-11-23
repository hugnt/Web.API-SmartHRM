using Microsoft.AspNetCore.Mvc;
using SmartHRM.Admin.Attributes;

namespace SmartHRM.Admin.Controllers
{
	public class TaskController : Controller
	{
        [Route("Tasks/Project")]
        public IActionResult Project()
		{
			return View("~/Views/Task/Project.cshtml");
		}

        [Route("Tasks/Project/Trash")]
        public IActionResult ProjectTrash()
        {
            return View("~/Views/Task/ProjectTrash.cshtml");
        }

        [Route("Tasks/TaskList")]
        public IActionResult Task()
        {
            return View("~/Views/Task/Task.cshtml");
        }

        
        [Route("/Tasks/TaskOfEmployeeDepartment")]
        public IActionResult TaskOfEmployeeDepartment()
        {
            return View("~/Views/Task/Index.cshtml");
        }


        [Route("Tasks/TaskList/Trash")]
        public IActionResult TaskTrash()
        {
            return View("~/Views/Task/TaskTrash.cshtml");
        }

        [CustomAuthorize("Employee")]
        [Route("/Tasks/TasksOfEmployee")]
        public IActionResult TasksOfEmployee()
        {
            return View("~/Views/Task/TasksOfEmployee.cshtml");
        }
    }
}

