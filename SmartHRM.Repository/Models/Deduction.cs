﻿using HUG.CRUD.Base;
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
        [Column("name")]
        public string? Name { get; set; }
        [Column("amount")]
        public decimal? amount { get; set; }
        [Column("expriredAt")]
        public DateTime? expriredAt { get; set; }
        [Column("note")]
        public string? note { get; set; }

    }
}
