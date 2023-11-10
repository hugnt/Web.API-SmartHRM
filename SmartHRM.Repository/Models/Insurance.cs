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
    [Table("tInsurance")]
    public class Insurance:BaseModel
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("benefits")]
        public string? Benefits { get; set; }
        [Column("requirement")]
        public string? Requirement { get; set; }
        [Column("amount")]
        public decimal? Amount { get; set; }
    }
}
