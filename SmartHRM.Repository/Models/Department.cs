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
    [Table("tDepartment")]
    public class Department : BaseModel
    {
        [Column("name")]
        public string? Name { get; set; }
		[Column("manager_id")]
        public int ManagerId { get; set; }
		[Column("description")]
        public string? Description { get; set; }
    }
}
