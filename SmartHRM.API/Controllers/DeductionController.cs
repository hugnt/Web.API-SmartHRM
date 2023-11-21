﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionController : ControllerBase
    {
        //sample
        private readonly DeductionService _DeductionService;
        public DeductionController(DeductionService DeductionService)
        {
            _DeductionService = DeductionService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Deduction>))]
        public IActionResult GetDeductions()
        {
            var Deductions = _DeductionService.GetDeductions();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Deductions);
        }

        [HttpGet("{DeductionId}")]
        [ProducesResponseType(200, Type = typeof(Deduction))]
        [ProducesResponseType(400)]
        public IActionResult GetDeduction(int DeductionId)
        {
            var Deduction = _DeductionService.GetDeduction(DeductionId);
            if (!ModelState.IsValid) return BadRequest();
            if (Deduction == null) return NotFound();
            return Ok(Deduction);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateDeduction([FromBody] Deduction DeductionCreate)
        {
            if (DeductionCreate == null) return BadRequest(ModelState);

            var res = _DeductionService.CreateDeduction(DeductionCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{DeductionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeduction(int DeductionId, [FromBody] Deduction updatedDeduction)
        {
            if (updatedDeduction == null) return BadRequest(ModelState);
            if (DeductionId != updatedDeduction.Id) return BadRequest(ModelState);


            var res = _DeductionService.UpdateDeduction(DeductionId, updatedDeduction);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{DeductionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDeduction(int DeductionId)
        {
            var res = _DeductionService.DeleteDeduction(DeductionId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }
        [HttpPut("DeletedStatus/{deductionId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int deductionId, bool status)
        {

            var res = _DeductionService.UpdateDeleteStatus(deductionId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Deduction>))]
        public IActionResult Search(string field, string keyWords)
        {
            var deductions = _DeductionService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(deductions);
        }


    }
}
