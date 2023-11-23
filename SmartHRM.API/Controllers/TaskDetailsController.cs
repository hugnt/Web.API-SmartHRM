using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {
        //sample
        private readonly TaskDetailsService _TaskDetailsService;
        public TaskDetailsController(TaskDetailsService TaskDetailsService)
        {
            _TaskDetailsService = TaskDetailsService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDetails>))]
        public IActionResult GetTaskDetailss()
        {
            var TaskDetailss = _TaskDetailsService.GetTaskDetailss();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(TaskDetailss);
        }

        [HttpGet("EmployeeDepartment/{employeeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDetails>))]
        public IActionResult GetTaskDetailsByEmployeeDepartment(int employeeId)
        {
            var TaskDetailss = _TaskDetailsService.GetTaskDetailsByEmployeeDepartment(employeeId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(TaskDetailss);
        }


        [HttpGet("{TaskDetailsId}")]
        [ProducesResponseType(200, Type = typeof(TaskDetails))]
        [ProducesResponseType(400)]
        public IActionResult GetTaskDetails(int TaskDetailsId)
        {
            var TaskDetails = _TaskDetailsService.GetTaskDetails(TaskDetailsId);
            if (!ModelState.IsValid) return BadRequest();
            if (TaskDetails == null) return NotFound();
            return Ok(TaskDetails);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTaskDetails([FromBody] TaskDetails TaskDetailsCreate)
        {
            if (TaskDetailsCreate == null) return BadRequest(ModelState);

            var res = _TaskDetailsService.CreateTaskDetails(TaskDetailsCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{TaskDetailsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTaskDetails(int TaskDetailsId, [FromBody] TaskDetails updatedTaskDetails)
        {
            if (updatedTaskDetails == null) return BadRequest(ModelState);
            if (TaskDetailsId != updatedTaskDetails.Id) return BadRequest(ModelState);


            var res = _TaskDetailsService.UpdateTaskDetails(TaskDetailsId, updatedTaskDetails);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{TaskDetailsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTaskDetails(int TaskDetailsId)
        {
            var res = _TaskDetailsService.DeleteTaskDetails(TaskDetailsId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }


        [HttpPut("DeletedStatus/{TaskDetailsId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int TaskDetailsId, bool status)
        {

            var res = _TaskDetailsService.UpdateDeleteStatus(TaskDetailsId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TaskDetails>))]
        public IActionResult Search(string field, string keyWords)
        {
            var TaskDetailss = _TaskDetailsService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(TaskDetailss);
        }


    }
}
