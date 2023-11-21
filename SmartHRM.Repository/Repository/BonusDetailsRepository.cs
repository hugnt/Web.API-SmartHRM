using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class BonusDetailsRepository : GenericRepository<BonusDetails>
    {
        private readonly AppDbContext _dbContext;
        public BonusDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsExists(BonusDetails entity)
        {
            var checkExist = _dbContext.BonusDetails.Any(x => x.EmployeeId == entity.EmployeeId
                                                   && x.BonusId == entity.BonusId
                                                   && x.StartAt.Month == entity.StartAt.Month
                                                   && x.StartAt.Year == entity.StartAt.Year);
            return checkExist;
        }

    }
}
