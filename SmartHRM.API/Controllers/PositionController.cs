using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        //sample
        private readonly PositionService _PositionService;
        public PositionController(PositionService PositionService)
        {
            _PositionService = PositionService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Position>))]
        public IActionResult GetPositions()
        {
            var Positions = _PositionService.GetPositions();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Positions);
        }

        [HttpGet("{PositionId}")]
        [ProducesResponseType(200, Type = typeof(Position))]
        [ProducesResponseType(400)]
        public IActionResult GetPosition(int PositionId)
        {
            var Position = _PositionService.GetPosition(PositionId);
            if (!ModelState.IsValid) return BadRequest();
            if (Position == null) return NotFound();
            return Ok(Position);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreatePosition([FromBody] Position PositionCreate)
        {
            if (PositionCreate == null) return BadRequest(ModelState);

            var res = _PositionService.CreatePosition(PositionCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{PositionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePosition(int PositionId, [FromBody] Position updatedPosition)
        {
            if (updatedPosition == null) return BadRequest(ModelState);
            if (PositionId != updatedPosition.Id) return BadRequest(ModelState);


            var res = _PositionService.UpdatePosition(PositionId, updatedPosition);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{PositionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePosition(int PositionId)
        {
            var res = _PositionService.DeletePosition(PositionId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }
        [HttpPut("DeletedStatus/{positionId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int positionId, bool status)
        {

            var res = _PositionService.UpdateDeleteStatus(positionId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Position>))]
        public IActionResult Search(string field, string keyWords)
        {
            var positions = _PositionService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(positions);
        }

    }
}
