using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using SmartHRM.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class IsuranceRepository : GenericRepository<Isurance>
    {
        private readonly AppDbContext _dbContext;
        public IsuranceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
