using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //sample
        private readonly EmployeeService _employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetEmployees();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int employeeId)
        {
            var employee = _employeeService.GetEmployee(employeeId);
            if (!ModelState.IsValid) return BadRequest();
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] Employee employeeCreate)
        {
            if (employeeCreate == null) return BadRequest(ModelState);

            var res = _employeeService.CreateEmployee(employeeCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] Employee updatedEmployee)
        {
            if (updatedEmployee == null) return BadRequest(ModelState);
            if (employeeId != updatedEmployee.Id) return BadRequest(ModelState);


            var res = _employeeService.UpdateEmployee(employeeId, updatedEmployee);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            var res = _employeeService.DeleteEmployee(employeeId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }

        [HttpGet("Statistic/Total")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalEmployee()
        {
            var res = _employeeService.GetTotal();
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }

        [HttpGet("Statistic/MaleFemale")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetStatisticMaleFemale()
        {
            var res = _employeeService.GetStatisticMaleFemale();
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet("Statistic/TopYoungest/{limit}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetTopAmountInsurance(int limit)
        {
            var res = _employeeService.GetTopYoungest(limit);
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }
    }
}
