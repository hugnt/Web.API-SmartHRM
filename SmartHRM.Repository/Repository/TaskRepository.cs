using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
	public class TaskRepository : GenericRepository<Task>
	{
		private readonly AppDbContext _dbContext;
		public TaskRepository(AppDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

        //Get total Time /Task
        public double GetTotalTaskTime()
        {
            List<Task> tasks = _dbContext.Tasks.ToList();
            TimeSpan totalDuration = new TimeSpan();

            foreach (var task in tasks)
            {
                if (task.StartTime.HasValue && task.IsDeleted == false)
                {
                    if (DateTime.Now > task.StartTime.Value)
                    {
                        TimeSpan projectDuration = DateTime.Now - task.StartTime.Value;
                        totalDuration += projectDuration;
                    }
                }
            }
            return (double)Math.Round(totalDuration.TotalHours);
        }
    } 
}