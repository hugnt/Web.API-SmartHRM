using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;
using SmartHRM.Services.Models;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        //sample
        private readonly InsuranceService _insuranceService;
        public InsuranceController(InsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Insurance>))]
        public IActionResult GetInsurances()
        {
            var insurances = _insuranceService.GetInsurances();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(insurances);
        }

        [HttpGet("{insuranceId}")]
        [ProducesResponseType(200, Type = typeof(Insurance))]
        [ProducesResponseType(400)]
        public IActionResult GetInsurance(int insuranceId)
        {
            var insurance = _insuranceService.GetInsurance(insuranceId);
            if (!ModelState.IsValid) return BadRequest();
            if (insurance == null) return NotFound();
            return Ok(insurance);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateInsurance([FromBody] Insurance insuranceCreate)
        {
            if (insuranceCreate == null) return BadRequest(ModelState);

            var res = _insuranceService.CreateInsurance(insuranceCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{insuranceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInsurance(int insuranceId, [FromBody] Insurance updatedInsurance)
        {
            if (updatedInsurance == null) return BadRequest(ModelState);
            if (insuranceId != updatedInsurance.Id) return BadRequest(ModelState);


            var res = _insuranceService.UpdateInsurance(insuranceId, updatedInsurance);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{insuranceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInsurance(int insuranceId)
        {
            var res = _insuranceService.DeleteInsurance(insuranceId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }


        [HttpPut("DeletedStatus/{insuranceId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int insuranceId, bool status)
        {

            var res = _insuranceService.UpdateDeleteStatus(insuranceId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Insurance>))]
        public IActionResult Search(string field, string keyWords)
        {
            var insurances = _insuranceService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(insurances);
        }


    }
}
