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
    public class PositionRepository : GenericRepository<Position>
    {
        private readonly AppDbContext _dbContext;
        public PositionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}