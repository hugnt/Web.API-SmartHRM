using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tContract")]
    public class Contract : BaseModel
    {
        [Column("employee_id")]
        public int? EmployeeId { get; set; }
        public DateTime? SignedAt { get; set; }
        public DateTime? ExpriedAt { get; set; }
        public string? Image {  get; set; }
        public string? Content { get; set; }

    }
}
