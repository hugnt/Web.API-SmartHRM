using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;
using Task = SmartHRM.Repository.Task;

namespace SmartHRM.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		//sample
		private readonly TaskService _TaskService;
		public TaskController(TaskService TaskService)
		{
			_TaskService = TaskService;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
		public IActionResult GetTasks()
		{
			var Tasks = _TaskService.GetTasks();
			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(Tasks);
		}

		[HttpGet("{TaskId}")]
		[ProducesResponseType(200, Type = typeof(Task))]
		[ProducesResponseType(400)]
		public IActionResult GetTask(int TaskId)
		{
			var Task = _TaskService.GetTask(TaskId);
			if (!ModelState.IsValid) return BadRequest();
			if (Task == null) return NotFound();
			return Ok(Task);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public IActionResult CreateTask([FromBody] Task TaskCreate)
		{
			if (TaskCreate == null) return BadRequest(ModelState);

			var res = _TaskService.CreateTask(TaskCreate);

			if (res.Status != 201)
			{
				ModelState.AddModelError("", res.StatusMessage);
				return StatusCode(res.Status, ModelState);
			}

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return StatusCode(res.Status, res.StatusMessage);
		}

		[HttpPut("{TaskId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateTask(int TaskId, [FromBody] Task updatedTask)
		{
			if (updatedTask == null) return BadRequest(ModelState);
			if (TaskId != updatedTask.Id) return BadRequest(ModelState);


			var res = _TaskService.UpdateTask(TaskId, updatedTask);
			if (res.Status != 204)
			{
				ModelState.AddModelError("", res.StatusMessage);
				return StatusCode(res.Status, ModelState);

			}
			if (!ModelState.IsValid) return BadRequest(ModelState);
			return NoContent();
		}

		[HttpDelete("{TaskId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteTask(int TaskId)
		{
			var res = _TaskService.DeleteTask(TaskId);
			if (res.Status != 204)
			{
				ModelState.AddModelError("", res.StatusMessage);
				return StatusCode(res.Status, ModelState);
			}

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return NoContent();
		}

        [HttpPut("DeletedStatus/{TaskId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int TaskId, bool status)
        {

            var res = _TaskService.UpdateDeleteStatus(TaskId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
        public IActionResult Search(string field, string keyWords)
        {
            var Tasks = _TaskService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(Tasks);
        }

    }
}