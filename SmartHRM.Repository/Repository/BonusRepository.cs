using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class BonusRepository: GenericRepository<Bonus>
    {
        private readonly AppDbContext _dbContext;
        public BonusRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsExists(Bonus Bonus)
        {
            var user = _dbContext.Bonus.SingleOrDefault(a =>
                a.Name == Bonus.Name
            );
            return user != null;
        }

        public Bonus GetBonus(Bonus Bonus)
        {
            return _dbContext.Bonus.SingleOrDefault(a => a.Name == Bonus.Name);
        }
    }
}
