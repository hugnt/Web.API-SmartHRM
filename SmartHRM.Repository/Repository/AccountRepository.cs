using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class AccountRepository: GenericRepository<Account>
    {
        private readonly AppDbContext _dbContext;
        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsExists(Account account)
        {
            var user = _dbContext.Accounts.SingleOrDefault(a =>
                a.Username == account.Username
            );
            return user != null;
        }

        public Account GetAccount(Account account)
        {
            return _dbContext.Accounts.SingleOrDefault(a => a.Username == account.Username);
        }
    }
}
