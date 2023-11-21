using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class TaskDetailsRepository : GenericRepository<TaskDetails>
    {
        private readonly AppDbContext _dbContext;
        public TaskDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsExists(TaskDetails entity)
        {
            var checkExist = _dbContext.TaskDetails.Any(x => x.AssigneeId == entity.AssigneeId
                                                   && x.AssignerId == entity.AssignerId
                                                   && x.TaskId == entity.TaskId);
            return checkExist;
        }

    }
}
