using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tTimeKeeping")]
    public class TimeKeeping : BaseModel
    {
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        public DateTime TimeAttendance { get; set; }
        public string Note { get; set; }


    }
}
