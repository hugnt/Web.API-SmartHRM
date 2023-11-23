using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class AllowanceDetailsRepository : GenericRepository<AllowanceDetails>
    {
        private readonly AppDbContext _dbContext;
        public AllowanceDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsExists(AllowanceDetails entity)
        {
            var checkExist = _dbContext.AllowanceDetails.Any(x => x.EmployeeId == entity.EmployeeId
                                                   && x.AllowanceId == entity.AllowanceId
                                                   && x.StartAt.Month == entity.StartAt.Month
                                                   && x.StartAt.Year == entity.StartAt.Year);
            return checkExist;
        }

    }
}
