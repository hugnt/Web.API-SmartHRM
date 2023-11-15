using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tDepartment")]
    public class Department : BaseModel
    {
        public string? Name { get; set; }
        [Column("manager_id")]
        public int? Manager_id { get; set; }
        public string? Description {  get; set; }
    }
}
