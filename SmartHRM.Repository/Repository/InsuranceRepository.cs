using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class InsuranceRepository: GenericRepository<Insurance>
    {
        private readonly AppDbContext _dbContext;
        public InsuranceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
