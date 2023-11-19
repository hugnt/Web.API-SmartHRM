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

	}
}