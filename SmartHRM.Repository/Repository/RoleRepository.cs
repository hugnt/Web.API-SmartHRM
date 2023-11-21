using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class RoleRepository: GenericRepository<Role>
    {
        private readonly AppDbContext _dbContext;
        public RoleRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

    }
}
