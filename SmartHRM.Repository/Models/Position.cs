using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository.Models
{
    [Table("tPosition")]
    public class Position : BaseModel
    {
        [Column("name")]
        public string Name { get; set; } 

    }
}
