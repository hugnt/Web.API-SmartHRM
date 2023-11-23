using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceDetailsController : ControllerBase
    {
        //sample
        private readonly InsuranceDetailsService _insuranceDetailsService;
        public InsuranceDetailsController(InsuranceDetailsService insuranceDetailsService)
        {
            _insuranceDetailsService = insuranceDetailsService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InsuranceDetails>))]
        public IActionResult GetInsuranceDetailss()
        {
            var insuranceDetailss = _insuranceDetailsService.GetInsuranceDetailss();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(insuranceDetailss);
        }

        [HttpGet("{insuranceDetailsId}")]
        [ProducesResponseType(200, Type = typeof(InsuranceDetails))]
        [ProducesResponseType(400)]
        public IActionResult GetInsuranceDetails(int insuranceDetailsId)
        {
            var insuranceDetails = _insuranceDetailsService.GetInsuranceDetails(insuranceDetailsId);
            if (!ModelState.IsValid) return BadRequest();
            if (insuranceDetails == null) return NotFound();
            return Ok(insuranceDetails);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateInsuranceDetails([FromBody] InsuranceDetails insuranceDetailsCreate)
        {
            if (insuranceDetailsCreate == null) return BadRequest(ModelState);

            var res = _insuranceDetailsService.CreateInsuranceDetails(insuranceDetailsCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{insuranceDetailsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInsuranceDetails(int insuranceDetailsId, [FromBody] InsuranceDetails updatedInsuranceDetails)
        {
            if (updatedInsuranceDetails == null) return BadRequest(ModelState);
            if (insuranceDetailsId != updatedInsuranceDetails.Id) return BadRequest(ModelState);


            var res = _insuranceDetailsService.UpdateInsuranceDetails(insuranceDetailsId, updatedInsuranceDetails);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{insuranceDetailsId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInsuranceDetails(int insuranceDetailsId)
        {
            var res = _insuranceDetailsService.DeleteInsuranceDetails(insuranceDetailsId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }


        [HttpPut("DeletedStatus/{insuranceDetailsId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int insuranceDetailsId, bool status)
        {

            var res = _insuranceDetailsService.UpdateDeleteStatus(insuranceDetailsId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InsuranceDetails>))]
        public IActionResult Search(string field, string keyWords)
        {
            var insuranceDetailss = _insuranceDetailsService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(insuranceDetailss);
        }


    }
}
