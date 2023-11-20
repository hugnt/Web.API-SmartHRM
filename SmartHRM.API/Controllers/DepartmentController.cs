using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using SmartHRM.Services;
using SmartHRM.Services.Models;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //sample
        private readonly DepartmentService _DepartmentService;
        public DepartmentController(DepartmentService DepartmentService)
        {
            _DepartmentService = DepartmentService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentDto>))]
        public IActionResult GetDepartments()
        {
            var Departments = _DepartmentService.GetDepartments();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Departments);
        }

        [HttpGet("{DepartmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int DepartmentId)
        {
            var Department = _DepartmentService.GetDepartment(DepartmentId);
            if (!ModelState.IsValid) return BadRequest();
            if (Department == null) return NotFound();
            return Ok(Department);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] Department DepartmentCreate)
        {
            if (DepartmentCreate == null) return BadRequest(ModelState);

            var res = _DepartmentService.CreateDepartment(DepartmentCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{DepartmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int DepartmentId, [FromBody] Department updatedDepartment)
        {
            if (updatedDepartment == null) return BadRequest(ModelState);
            if (DepartmentId != updatedDepartment.Id) return BadRequest(ModelState);


            var res = _DepartmentService.UpdateDepartment(DepartmentId, updatedDepartment);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{DepartmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int DepartmentId)
        {
            var res = _DepartmentService.DeleteDepartment(DepartmentId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }
        [HttpPut("DeletedStatus/{departmentId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int departmentId, bool status)
        {

            var res = _DepartmentService.UpdateDeleteStatus(departmentId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult Search(string field, string keyWords)
        {
            var departments = _DepartmentService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(departments);
        }
        [HttpGet("Statistic/Total")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalEmployee()
        {
            var res = _DepartmentService.GetTotal();
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }
        [HttpGet("Statistic/EmployeeCount")]
        [ProducesResponseType(200, Type = typeof(Dictionary<string, int>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeCountByDepartment()
        {
            var employeeCountByDepartment = _DepartmentService.GetEmployeeCountByDepartment();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(employeeCountByDepartment);
        }
        [HttpGet("CurrentDepartment/{employeeId}")]
        [ProducesResponseType(200, Type = typeof(DepartmentDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetCurrentDepartment(int employeeId)
        {
            var currentDepartment = _DepartmentService.GetCurrentDepartmentForEmployee(employeeId);

            if (currentDepartment == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(currentDepartment);
        }

    }
}
