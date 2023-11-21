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
    public class EmployeeDto : Employee
    {  
        public string? Position { get; set; }
        public string? Department { get; set; }
  
    }
}
