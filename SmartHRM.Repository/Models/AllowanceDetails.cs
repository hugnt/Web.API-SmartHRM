﻿using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tAllowanceDetails")]
    public class AllowanceDetails : BaseModel
    {
		[Column("allowance_id")]
        public int AllowanceID { get; set; }
        [Column("employee_id")]
        public int EmployeeID { get; set; }
        [Column("startAt")]
        public DateTime StartAt { get; set; }

    }
}