using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Services;
using SmartHRM.Services.Models;
using System.Web;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly SalaryService _salaryService;
        public SalaryController(SalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet("ListSalary")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetAllSalary()
        {
            var res = _salaryService.GetAllSalary();
            if (!ModelState.IsValid) return BadRequest();
            if (res.Count == 0) return NotFound();
            return Ok(res);
        }

        [HttpGet("ListSalary/{employeeId}/{monthYear}")]
        [ProducesResponseType(200, Type = typeof(EmployeeSalaryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetSalary(int employeeId, string monthYear)
        {
            string decodedString = HttpUtility.UrlDecode(monthYear);
            var res = _salaryService.GetSalary(employeeId, decodedString);
            if (!ModelState.IsValid) return BadRequest();
            //if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpPost("ListSalary")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddOrUpdateSalary([FromBody] SalaryDto salaryDto)
        {
            if (salaryDto == null) return BadRequest(ModelState);

            var res = _salaryService.AddOrUpdateSalary(salaryDto);

            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpDelete("ListSalary/Delete/{employeeId}/{monthYear}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSalary(int employeeId, string monthYear)
        {
            string decodedString = HttpUtility.UrlDecode(monthYear);
            var res = _salaryService.DeleteSalary(employeeId, decodedString);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }

    }
}
