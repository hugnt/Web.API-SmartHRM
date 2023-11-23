using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class InsuranceDetailsRepository : GenericRepository<InsuranceDetails>
    {
        private readonly AppDbContext _dbContext;
        public InsuranceDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsExists(InsuranceDetails entity)
        {
            var checkExist = _dbContext.InsuranceDetails.Any(x => x.EmployeeId == entity.EmployeeId
                                                   && x.InsuranceId == entity.InsuranceId
                                                   && x.ProvideAt.Month == entity.ProvideAt.Month
                                                   && x.ProvideAt.Year == entity.ProvideAt.Year);
            return checkExist;
        }

    }
}
