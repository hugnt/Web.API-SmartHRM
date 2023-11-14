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
        private readonly PositionService _positionService;
        public PositionController(PositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Position>))]
        public IActionResult GetPositions()
        {
            var positions = _positionService.GetPositions();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(positions);
        }

        [HttpGet("{positionId}")]
        [ProducesResponseType(200, Type = typeof(Position))]
        [ProducesResponseType(400)]
        public IActionResult GetPosition(int positionId)
        {
            var position = _positionService.GetPosition(positionId);
            if (!ModelState.IsValid) return BadRequest();
            if (position == null) return NotFound();
            return Ok(position);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreatePosition([FromBody] Position positionCreate)
        {
            if (positionCreate == null) return BadRequest(ModelState);

            var res = _positionService.CreatePosition(positionCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePosition(int positionId, [FromBody] Position updatedPosition)
        {
            if (updatedPosition == null) return BadRequest(ModelState);
            if (positionId != updatedPosition.Id) return BadRequest(ModelState);


            var res = _positionService.UpdatePosition(positionId, updatedPosition);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePosition(int positionId)
        {
            var res = _positionService.DeletePosition(positionId);
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

            var res = _positionService.UpdateDeleteStatus(positionId, status);
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
            var positions = _positionService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(positions);
        }


    }
}
