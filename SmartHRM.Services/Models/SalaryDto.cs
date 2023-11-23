using HUG.CRUD.Base;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class SalaryDto
    {
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public double CoefficientSalary { get; set; }
        public List<AllowanceDetails> Allowances { get; set; }
        public List<BonusDetails> Bonus { get; set; }
        public List<DeductionDetails> Deduction { get; set; }
        public DateTime MonthYear { get; set; }

    }
}
