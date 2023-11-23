using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class AllowanceRepository: GenericRepository<Allowance>
    {
        private readonly AppDbContext _dbContext;
        public AllowanceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsExists(Allowance Allowance)
        {
            var user = _dbContext.Allowances.SingleOrDefault(a =>
                a.Name == Allowance.Name
            );
            return user != null;
        }

        public Allowance GetAllowance(Allowance Allowance)
        {
            return _dbContext.Allowances.SingleOrDefault(a => a.Name == Allowance.Name);
        }
    }
}
