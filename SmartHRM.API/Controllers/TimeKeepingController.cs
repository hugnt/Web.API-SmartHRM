using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeKeepingController : ControllerBase
    {
        //sample
        private readonly TimeKeepingService _TimeKeepingService;
        public TimeKeepingController(TimeKeepingService TimeKeepingService)
        {
            _TimeKeepingService = TimeKeepingService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TimeKeeping>))]
        public IActionResult GetTimeKeepings()
        {
            var TimeKeepings = _TimeKeepingService.GetTimeKeepings();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(TimeKeepings);
        }

        [HttpGet("{TimeKeepingId}")]
        [ProducesResponseType(200, Type = typeof(TimeKeeping))]
        [ProducesResponseType(400)]
        public IActionResult GetTimeKeeping(int TimeKeepingId)
        {
            var TimeKeeping = _TimeKeepingService.GetTimeKeeping(TimeKeepingId);
            if (!ModelState.IsValid) return BadRequest();
            if (TimeKeeping == null) return NotFound();
            return Ok(TimeKeeping);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTimeKeeping([FromBody] TimeKeeping TimeKeepingCreate)
        {
            if (TimeKeepingCreate == null) return BadRequest(ModelState);

            var res = _TimeKeepingService.CreateTimeKeeping(TimeKeepingCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{TimeKeepingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTimeKeeping(int TimeKeepingId, [FromBody] TimeKeeping updatedTimeKeeping)
        {
            if (updatedTimeKeeping == null) return BadRequest(ModelState);
            if (TimeKeepingId != updatedTimeKeeping.Id) return BadRequest(ModelState);


            var res = _TimeKeepingService.UpdateTimeKeeping(TimeKeepingId, updatedTimeKeeping);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{TimeKeepingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTimeKeeping(int TimeKeepingId)
        {
            var res = _TimeKeepingService.DeleteTimeKeeping(TimeKeepingId);
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
