﻿using HUG.CRUD.Base;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class TimeKeeperEmployee:BaseModel
    {
        public DateTime? TimeAttendance { get; set; }
        public string? Note { get; set; }
        public Employee EmployeeName {  get; set; }
    }
}
