using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        //sample
        private readonly InsuranceService _InsuranceService;
        public InsuranceController(InsuranceService InsuranceService)
        {
            _InsuranceService = InsuranceService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Insurance>))]
        public IActionResult GetInsurances()
        {
            var Insurances = _InsuranceService.GetInsurances();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Insurances);
        }

        [HttpGet("{InsuranceId}")]
        [ProducesResponseType(200, Type = typeof(Insurance))]
        [ProducesResponseType(400)]
        public IActionResult GetInsurance(int InsuranceId)
        {
            var Insurance = _InsuranceService.GetInsurance(InsuranceId);
            if (!ModelState.IsValid) return BadRequest();
            if (Insurance == null) return NotFound();
            return Ok(Insurance);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateInsurance([FromBody] Insurance InsuranceCreate)
        {
            if (InsuranceCreate == null) return BadRequest(ModelState);

            var res = _InsuranceService.CreateInsurance(InsuranceCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{InsuranceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInsurance(int InsuranceId, [FromBody] Insurance updatedInsurance)
        {
            if (updatedInsurance == null) return BadRequest(ModelState);
            if (InsuranceId != updatedInsurance.Id) return BadRequest(ModelState);


            var res = _InsuranceService.UpdateInsurance(InsuranceId, updatedInsurance);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{InsuranceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInsurance(int InsuranceId)
        {
            var res = _InsuranceService.DeleteInsurance(InsuranceId);
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