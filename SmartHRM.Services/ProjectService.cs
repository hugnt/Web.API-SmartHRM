using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SmartHRM.Services
{
	public class ProjectService
	{
		private readonly ProjectRepository _ProjectRepository;
		public ProjectService(ProjectRepository ProjectRepository)
		{
			_ProjectRepository = ProjectRepository;
		}
		public ResponseModel CreateProject(Project ProjectCreate)
		{
			var Projects = _ProjectRepository.GetAll()
							.Where(l => l.Name.Trim().ToLower() == ProjectCreate.Name.Trim().ToLower())
							.FirstOrDefault();
			if (Projects != null)
			{
				return new ResponseModel(422, "Project already exists");
			}

			if (!_ProjectRepository.Create(ProjectCreate))
			{
				return new ResponseModel(500, "Something went wrong while saving");
			}

			return new ResponseModel(201, "Successfully created");
		}

		public ResponseModel DeleteProject(int ProjectId)
		{
			if (!_ProjectRepository.IsExists(ProjectId)) return new ResponseModel(404, "Not found");
			var ProjectToDelete = _ProjectRepository.GetById(ProjectId);
			if (!_ProjectRepository.Delete(ProjectToDelete))
			{
				return new ResponseModel(500, "Something went wrong when deleting Project");
			}
			return new ResponseModel(204, "");
		}

		public Project? GetProject(int ProjectId)
		{
			if (!_ProjectRepository.IsExists(ProjectId)) return null;
			var Project = _ProjectRepository.GetById(ProjectId);
			return Project;
		}

		public IEnumerable<Project> GetProjects()
		{
			return _ProjectRepository.GetAll();
		}

		public ResponseModel UpdateProject(int ProjectId, Project updatedProject)
		{
			if (!_ProjectRepository.IsExists(ProjectId)) return new ResponseModel(404, "Not found");

			if (!_ProjectRepository.Update(updatedProject))
			{
				return new ResponseModel(500, "Something went wrong updating Project");
			}
			return new ResponseModel(204, "");
		}

        public ResponseModel UpdateDeleteStatus(int ProjectId, bool status)
        {
            if (!_ProjectRepository.IsExists(ProjectId)) return new ResponseModel(404, "Not found");
            var updatedProject = _ProjectRepository.GetById(ProjectId);
            updatedProject.IsDeleted = status;
            if (!_ProjectRepository.Update(updatedProject))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Project");
            }
            return new ResponseModel(204, "");
        }

        public IEnumerable<Project> Search(string field, string keyWords)
        {
            if (keyWords == "null") return _ProjectRepository.GetAll();
            var res = _ProjectRepository.Search(field, keyWords);
            if (res == null) return new List<Project>();
            return res;
        }

    }
}