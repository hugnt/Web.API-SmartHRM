using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly StatisticalService _statisticalService;
        public StatisticalController(StatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }

        [HttpGet("Employee/Total")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalEmployee()
        {
            var res = _statisticalService.GetTotalEmployee();
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }

        [HttpGet("Employee/MaleFemale")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetStatisticMaleFemale()
        {
            var res = _statisticalService.GetStatisticMaleFemale();
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet("Employee/TopAmountInsurance/{limit}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetTopAmountInsurance(int limit)
        {
            var res = _statisticalService.GetTopAmountInsurance(limit);
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }
    }
}
