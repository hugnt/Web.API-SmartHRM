using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
	public class ProjectRepository : GenericRepository<Project>
	{
		private readonly AppDbContext _dbContext;
		public ProjectRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

        //Get total Time /Project
        public int GetTotalProjectTime()
        {
            List<Project> projects = _dbContext.Projects.ToList();
            TimeSpan totalDuration = new TimeSpan();

            foreach (var project in projects)
            {
                if (project.StartedAt.HasValue && project.IsDeleted == false)
                {
                    DateTime endDate = project.EndedAt.HasValue ? project.EndedAt.Value : DateTime.Now;
                    if (endDate > project.StartedAt.Value)
                    {
                        TimeSpan projectDuration = endDate - project.StartedAt.Value;
                        totalDuration += projectDuration;
                    }
                }
            }

            return (int)Math.Round(totalDuration.TotalDays);
        }

    }
}