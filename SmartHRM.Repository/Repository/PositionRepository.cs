using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class PositionRepository: GenericRepository<Position>
    {
        private readonly AppDbContext _dbContext;
        public PositionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsExists(Position Position)
        {
            var user = _dbContext.Position.SingleOrDefault(a =>
                a.Name == Position.Name
            );
            return user != null;
        }

        public Position GetPosition(Position Position)
        {
            return _dbContext.Position.SingleOrDefault(a => a.Name == Position.Name);
        }
    }
}
