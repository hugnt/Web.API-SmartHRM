using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
	public class ProjectService
	{
		private readonly ProjectRepository _ProjectRepository;
		public ProjectService(ProjectRepository ProjectRepository)
		{
			_ProjectRepository = ProjectRepository;
		}
		public ResponseModel CreateRole(Project ProjectCreate)
		{
			var Roles = _ProjectRepository.GetAll()
							.Where(l => l.Name.Trim().ToLower() == ProjectCreate.Name.Trim().ToLower())
							.FirstOrDefault();
			if (Roles != null)
			{
				return new ResponseModel(422, "Project already exists");
			}

			if (!_ProjectRepository.Create(ProjectCreate))
			{
				return new ResponseModel(500, "Something went wrong while saving");
			}

			return new ResponseModel(201, "Successfully created");
		}

		public ResponseModel DeleteRole(int RoleId)
		{
			if (!_ProjectRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");
			var RoleToDelete = _ProjectRepository.GetById(RoleId);
			if (!_ProjectRepository.Delete(RoleToDelete))
			{
				return new ResponseModel(500, "Something went wrong when deleting Project");
			}
			return new ResponseModel(204, "");
		}

		public Project? GetRole(int RoleId)
		{
			if (!_ProjectRepository.IsExists(RoleId)) return null;
			var Project = _ProjectRepository.GetById(RoleId);
			return Project;
		}

		public IEnumerable<Project> GetRoles()
		{
			return _ProjectRepository.GetAll();
		}

		public ResponseModel UpdateRole(int RoleId, Project updatedRole)
		{
			if (!_ProjectRepository.IsExists(RoleId)) return new ResponseModel(404, "Not found");

			if (!_ProjectRepository.Update(updatedRole))
			{
				return new ResponseModel(500, "Something went wrong updating Project");
			}
			return new ResponseModel(204, "");
		}


	}
}