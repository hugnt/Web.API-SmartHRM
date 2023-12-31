﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        //sample
        private readonly RoleService _RoleService;
        public RoleController(RoleService RoleService)
        {
            _RoleService = RoleService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        public IActionResult GetRoles()
        {
            var Roles = _RoleService.GetRoles();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Roles);
        }

        [HttpGet("{RoleId}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult GetRole(int RoleId)
        {
            var Role = _RoleService.GetRole(RoleId);
            if (!ModelState.IsValid) return BadRequest();
            if (Role == null) return NotFound();
            return Ok(Role);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateRole([FromBody] Role RoleCreate)
        {
            if (RoleCreate == null) return BadRequest(ModelState);

            var res = _RoleService.CreateRole(RoleCreate);

            if (res.Status != 201)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return StatusCode(res.Status, res.StatusMessage);
        }

        [HttpPut("{RoleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(int RoleId, [FromBody] Role updatedRole)
        {
            if (updatedRole == null) return BadRequest(ModelState);
            if (RoleId != updatedRole.Id) return BadRequest(ModelState);


            var res = _RoleService.UpdateRole(RoleId, updatedRole);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpDelete("{RoleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRole(int RoleId)
        {
            var res = _RoleService.DeleteRole(RoleId);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NoContent();
        }


        [HttpPut("DeletedStatus/{RoleId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int RoleId, bool status)
        {

            var res = _RoleService.UpdateDeleteStatus(RoleId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        public IActionResult Search(string field, string keyWords)
        {
            var Roles = _RoleService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(Roles);
        }

        [HttpGet("Statistic/Total")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalRole()
        {
            var res = _RoleService.GetTotal();
            if (!ModelState.IsValid) return BadRequest();
            return Ok(res);
        }

    }
}
