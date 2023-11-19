using HUG.CRUD.Base;
using HUG.CRUD.Interfaces;
using HUG.CRUD.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    public class StatisticalRepository
    {
        private readonly AppDbContext _dbContext;
        public StatisticalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get total record
        public int GetTotal<T>() where T : BaseModel
        {
            return _dbContext.Set<T>().Where(x=>x.IsDeleted==false).Count();
        }

        //Statistic 
        public object GetStatisticMaleFemale()
        {
            var maleCount = _dbContext.Employees.Where(x => x.Gender == true &&x.IsDeleted == false).Count();
            var femaleCount = _dbContext.Employees.Where(x => x.Gender == false && x.IsDeleted == false).Count();
            return new {
                Male = maleCount,
                Female = femaleCount
            };
        }


        //Get top / list
        public object GetTopAmountInsurance(int limit)
        {
            var res = _dbContext.Insurances.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Amount).Take(limit);
            return new { res };
        }


    }
}
