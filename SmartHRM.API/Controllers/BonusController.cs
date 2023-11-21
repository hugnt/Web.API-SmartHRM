using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
        private readonly BonusService _BonusService;
        public BonusController(BonusService BonusService)
        {
            _BonusService = BonusService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bonus>))]
        public IActionResult GetBonuss()
        {
            var Bonuss = _BonusService.GetBonuss();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Bonuss);
        }

        [HttpGet("{BonusId}")]
        [ProducesResponseType(200, Type = typeof(Bonus))]
        [ProducesResponseType(400)]
        public IActionResult GetBonus(int BonusId)
        {
            var Bonus = _BonusService.GetBonus(BonusId);
            if (!ModelState.IsValid) return BadRequest();
            if (Bonus == null) return NotFound();
            return Ok(Bonus);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateBonus([FromBody] Bonus BonusCreate)
        {
            if (BonusCreate == null) return BadRequest(ModelState);

            var res = _BonusService.CreateBonus(BonusCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{BonusId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBonus(int BonusId, [FromBody] Bonus updatedBonus)
        {
            if (updatedBonus == null) return BadRequest(ModelState);
            if (BonusId != updatedBonus.Id) return BadRequest(ModelState);


            var res = _BonusService.UpdateBonus(BonusId, updatedBonus);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{BonusId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBonus(int BonusId)
        {
            var res = _BonusService.DeleteBonus(BonusId);
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

            var res = _BonusService.UpdateDeleteStatus(BonusId, status);
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
            var Bonuss = _BonusService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(Bonuss);
        }
        [HttpGet("Statistic/TotalBonus")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalEmployee()
        {
            var res = _BonusService.GetTotal();
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }

        [HttpGet("Statistic/Month")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetStatisticMonth()
        {
            var res = _BonusService.GetStatisticMonth();
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet("Statistic/TopBonusHighest/{limit}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetTopAmountInsurance(int limit)
        {
            var res = _BonusService.GetTopBonusHighest(limit);
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }
    }
}
