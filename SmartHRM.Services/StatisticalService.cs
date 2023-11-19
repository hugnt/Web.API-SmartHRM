using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class StatisticalService 
    {
        private readonly StatisticalRepository _statisticalRepository;
        public StatisticalService(StatisticalRepository statisticalRepository)
        {
            _statisticalRepository = statisticalRepository;
        }

        //Total
        public int GetTotalEmployee() => _statisticalRepository.GetTotal<Employee>();

        //Statistic
        public object GetStatisticMaleFemale() => _statisticalRepository.GetStatisticMaleFemale();

        //Get top / list
        public object GetTopAmountInsurance(int limit) => _statisticalRepository.GetTopAmountInsurance(limit);

    }
}
