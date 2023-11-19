using HUG.CRUD.Base;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class ContractDto : BaseModel
    {
        public string? Content { get; set; }
        public DateTime? SignedAt { get; set; }
        public DateTime? ExpriedAt { get; set; }
        public string? Image { get; set; }
        public Employee Employees { get; set; }
    }
}
