using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tTask")]
    public class Task : BaseModel
    {
        public string? Name { get; set; }    
        public DateTime? StartTime { get; set; } 
        public int? Status { get; set; }
        public string? Content { get; set; }
        public string? Description {  get; set; }

    }
}
