﻿using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tBonus")]
    public class Bonus : BaseModel
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("amount")]
        public decimal? Amount { get; set; }
        [Column("expriredAt")]
        public DateTime? ExpriredAt { get; set; }
        [Column("note")]
        public string? Note { get; set; }

    }
}
