using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository.Models
{
    [Table("tContract")]
    public class Contract : BaseModel
    {
        [Column("content")]
        public string? Content { get; set; }
		[Column("employee_id")]
        public int EmployeeId { get; set; }
        [Column("signedAt")]
        public DateTime? SignedAt { get; set; }
        [Column("expriedAt")]
        public DateTime? ExpriedAt { get; set; }
        
		[Column("image")]
        public string? Image { get; set; }
        
        
    }
}
