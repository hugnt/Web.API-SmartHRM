using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class DeductionRepository: GenericRepository<Deduction>
    {
        private readonly AppDbContext _dbContext;
        public DeductionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsExists(Deduction Deduction)
        {
            var user = _dbContext.Deductions.SingleOrDefault(a =>
                a.Name == Deduction.Name
            );
            return user != null;
        }

        public Deduction GetDeduction(Deduction Deduction)
        {
            return _dbContext.Deductions.SingleOrDefault(a => a.Name == Deduction.Name);
        }
    }
}
