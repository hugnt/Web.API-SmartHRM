using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class RefeshTokenRepository: GenericRepository<RefeshToken>
    {
        private readonly AppDbContext _dbContext;
        public RefeshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public RefeshToken GetRefeshToken(string refeshToken)
        {
            return _dbContext.RefeshTokens.FirstOrDefault(x =>
               x.Token == refeshToken);
        }

        public bool IsExists(string refeshToken)
        {
            return _dbContext.RefeshTokens.Any(x =>
                x.Token == refeshToken);
        }

        public bool IsValid(string refeshToken)
        {
            var storedToken = _dbContext.RefeshTokens.FirstOrDefault(x =>
               x.Token == refeshToken);
            if (storedToken.ExpireAt < DateTime.UtcNow)
            {
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _dbContext.RefeshTokens.Update(storedToken);
                return false;
            }
            if (storedToken.IsUsed || storedToken.IsRevoked) return false;
            return true;
        }
    }
}
