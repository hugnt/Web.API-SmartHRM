using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tDeduction")]
    public class Deduction : BaseModel
    {
        public string? Name {  get; set; }
        public decimal? Amount { get; set; }

        [Column("expriredAt")]
        public DateTime? ExpriredAt { get; set; }
        public string? Note { get; set; }


    }
}
