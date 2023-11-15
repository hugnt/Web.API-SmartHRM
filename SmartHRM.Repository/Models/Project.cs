using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tProject")]
    public class Project : BaseModel
    {
        public string? Name { get; set; }
        [Column("leader_id")]
        public int? Leader_id { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int? Status { get; set; }

    }
}
