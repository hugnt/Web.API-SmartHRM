using Microsoft.AspNetCore.Http;
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

        [HttpGet("Statistic/GetAmountDeduction")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetAmountDeduction()
        {
            var res = _DeductionService.GetAmountDeduction();
            if(!ModelState.IsValid) return BadRequest(ModelState);  
            if(res == 0)   return NotFound();
            return Ok(res);
        }
        [HttpGet("Statistic/GetTotalAmountDeduction")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalAmountDeduction()
        {
            var res = _DeductionService.GetTotalAmountDeduction();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (res == 0) return NotFound();
            return Ok(res);
        }
        /*
        [HttpGet("Statistic/GetTotalDeduction/{month}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalDeduction(int month)
        {
            var res = _DeductionService.GetTotalDeduction(month);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (res == null) return NotFound();
            return Ok(res);
        }*/
        [HttpGet("Statistic/GetTopDeduction/{limit}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetTopDeduction(int limit)
        {
            var res = _DeductionService.GetTopDeduction(limit);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (res == null) return NotFound();
            return Ok(res);
        }
    }
}
