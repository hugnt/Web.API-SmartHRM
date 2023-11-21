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
    public class EmployeeSalaryDto : Employee
    {  
        public string? Position { get; set; }
        public string? Department { get; set; }
        public List<Allowance>? Allowances { get; set; }
        public List<int>? AllowanceDetailsId { get; set; }
        public List<Bonus>? Bonuss { get; set; }
        public List<int>? BonusDetailsId { get; set; }
        public List<Deduction>? Deductions { get; set; }
        public List<int>? DeductionDetailsId { get; set; }
        public int TimekeepingDay { get; set; }
        public string MonthYear { get; set; }
    }
}
