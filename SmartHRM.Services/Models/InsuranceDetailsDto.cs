using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class InsuranceDetailsDto:InsuranceDetails
    {
        public string FullName { get; set; }
        public string InsuranceName { get; set; }
        public decimal? InsuranceAmount { get; set; }

    }

}
