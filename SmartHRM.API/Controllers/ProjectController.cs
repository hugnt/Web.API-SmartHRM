using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHRM.Repository;
using SmartHRM.Services;

namespace SmartHRM.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		//sample
		private readonly ProjectService _RoleService;
		public ProjectController(ProjectService RoleService)
		{
			_RoleService = RoleService;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
		public IActionResult GetRoles()
		{
			var Roles = _RoleService.GetRoles();
			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(Roles);
		}

		[HttpGet("{RoleId}")]
		[ProducesResponseType(200, Type = typeof(Project))]
		[ProducesResponseType(400)]
		public IActionResult GetRole(int RoleId)
		{
			var Project = _RoleService.GetRole(RoleId);
			if (!ModelState.IsValid) return BadRequest();
			if (Project == null) return NotFound();
			return Ok(Project);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public IActionResult CreateRole([FromBody] Project RoleCreate)
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
		public IActionResult UpdateRole(int RoleId, [FromBody] Project updatedRole)
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


	}
}