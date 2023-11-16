using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowanceController : ControllerBase
    {
        //sample
        private readonly AllowanceService _AllowanceService;
        public AllowanceController(AllowanceService AllowanceService)
        {
            _AllowanceService = AllowanceService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Allowance>))]
        public IActionResult GetAllowances()
        {
            var Allowances = _AllowanceService.GetAllowances();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Allowances);
        }

        [HttpGet("{AllowanceId}")]
        [ProducesResponseType(200, Type = typeof(Allowance))]
        [ProducesResponseType(400)]
        public IActionResult GetAllowance(int AllowanceId)
        {
            var Allowance = _AllowanceService.GetAllowance(AllowanceId);
            if (!ModelState.IsValid) return BadRequest();
            if (Allowance == null) return NotFound();
            return Ok(Allowance);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAllowance([FromBody] Allowance AllowanceCreate)
        {
            if (AllowanceCreate == null) return BadRequest(ModelState);

            var res = _AllowanceService.CreateAllowance(AllowanceCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{AllowanceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAllowance(int AllowanceId, [FromBody] Allowance updatedAllowance)
        {
            if (updatedAllowance == null) return BadRequest(ModelState);
            if (AllowanceId != updatedAllowance.Id) return BadRequest(ModelState);


            var res = _AllowanceService.UpdateAllowance(AllowanceId, updatedAllowance);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{AllowanceId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAllowance(int AllowanceId)
        {
            var res = _AllowanceService.DeleteAllowance(AllowanceId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }

        [HttpPut("DeletedStatus/{BonusId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int BonusId, bool status)
        {

            var res = _AllowanceService.UpdateDeleteStatus(BonusId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bonus>))]
        public IActionResult Search(string field, string keyWords)
        {
            var Bonuss = _AllowanceService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(Bonuss);
        }
    }
}
