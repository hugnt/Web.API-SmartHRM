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
        [HttpPut("DeletedStatus/{timeKeepingId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int timeKeepingId, bool status)
        {

            var res = _TimeKeepingService.UpdateDeleteStatus(timeKeepingId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TimeKeeping>))]
        public IActionResult Search(string field, string keyWords)
        {
            var timeKeepings = _TimeKeepingService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(timeKeepings);
        }


        [HttpGet("Statistic/GetNumberOnTimeEmployee/{week}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetNumberOnTimeEmployee(int week)
        {
            
            var res = _TimeKeepingService.GetNumberOnTimeEmployee(week);
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
            
        }
        [HttpGet("Statistic/GetListOnTime/{week}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetListOnTime(int week)
        {
            var res = _TimeKeepingService.GetListOnTime(week);
            if (!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);

        }

        [HttpGet("Statistic/GetUsuallyLate/{limit}")]
        [ProducesResponseType(200, Type = typeof(object))]
        [ProducesResponseType(400)]
        public IActionResult GetListUsuallyLate(int limit)
        {
            var res = _TimeKeepingService.GetListUsuallyLate(limit);
            if(!ModelState.IsValid) return BadRequest();
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpGet("Statistic/GetNumberLate/{id}/{week}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetNumberLate(int id, int week)
        {
            var res = _TimeKeepingService.GetNumberLate(id, week);
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }

        [HttpGet("Statistic/GetNumberEmployeeNoWork/{week}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetNumberEmployeeNoWork(int week)
        {
            var res = _TimeKeepingService.GetNumberEmployeeNoWork(week);
            if (!ModelState.IsValid) return BadRequest();
            if (res == 0) return NotFound();
            return Ok(res);
        }
    }
}
