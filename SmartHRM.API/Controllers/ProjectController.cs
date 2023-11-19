﻿using Microsoft.AspNetCore.Http;
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
		private readonly ProjectService _ProjectService;
		public ProjectController(ProjectService ProjectService)
		{
			_ProjectService = ProjectService;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
		public IActionResult GetProjects()
		{
			var Projects = _ProjectService.GetProjects();
			if (!ModelState.IsValid) return BadRequest(ModelState);

			return Ok(Projects);
		}

		[HttpGet("{ProjectId}")]
		[ProducesResponseType(200, Type = typeof(Project))]
		[ProducesResponseType(400)]
		public IActionResult GetProject(int ProjectId)
		{
			var Project = _ProjectService.GetProject(ProjectId);
			if (!ModelState.IsValid) return BadRequest();
			if (Project == null) return NotFound();
			return Ok(Project);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public IActionResult CreateProject([FromBody] Project ProjectCreate)
		{
			if (ProjectCreate == null) return BadRequest(ModelState);

			var res = _ProjectService.CreateProject(ProjectCreate);

			if (res.Status != 201)
			{
				ModelState.AddModelError("", res.StatusMessage);
				return StatusCode(res.Status, ModelState);
			}

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return StatusCode(res.Status, res.StatusMessage);
		}

		[HttpPut("{ProjectId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult UpdateProject(int ProjectId, [FromBody] Project updatedProject)
		{
			if (updatedProject == null) return BadRequest(ModelState);
			if (ProjectId != updatedProject.Id) return BadRequest(ModelState);


			var res = _ProjectService.UpdateProject(ProjectId, updatedProject);
			if (res.Status != 204)
			{
				ModelState.AddModelError("", res.StatusMessage);
				return StatusCode(res.Status, ModelState);

			}
			if (!ModelState.IsValid) return BadRequest(ModelState);
			return NoContent();
		}

		[HttpDelete("{ProjectId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public IActionResult DeleteProject(int ProjectId)
		{
			var res = _ProjectService.DeleteProject(ProjectId);
			if (res.Status != 204)
			{
				ModelState.AddModelError("", res.StatusMessage);
				return StatusCode(res.Status, ModelState);
			}

			if (!ModelState.IsValid) return BadRequest(ModelState);

			return NoContent();
		}

        [HttpPut("DeletedStatus/{ProjectId}/{status}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeleteStatus(int ProjectId, bool status)
        {

            var res = _ProjectService.UpdateDeleteStatus(ProjectId, status);
            if (res.Status != 204)
            {
                ModelState.AddModelError("", res.StatusMessage);
                return StatusCode(res.Status, ModelState);

            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return NoContent();
        }

        [HttpGet("Search/{field}/{keyWords}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult Search(string field, string keyWords)
        {
            var Projects = _ProjectService.Search(field, keyWords);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(Projects);
        }

    }
}