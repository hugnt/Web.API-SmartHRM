using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class DeductionDetailsRepository : GenericRepository<DeductionDetails>
    {
        private readonly AppDbContext _dbContext;
        public DeductionDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsExists(DeductionDetails entity)
        {
            var checkExist = _dbContext.DeductionDetails.Any(x => x.EmployeeId == entity.EmployeeId
                                                   && x.DeductionId == entity.DeductionId
                                                   && x.StartAt.Month == entity.StartAt.Month
                                                   && x.StartAt.Year == entity.StartAt.Year);
            return checkExist;
        }

    }
}
