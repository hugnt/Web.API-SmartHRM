using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tTaskDetails")]
    public class TaskDetails : BaseModel
    {
        [Column("assignee_id")]
        public int AssigneeId { get; set; }
        [Column("assigner_id")]
        public int AssignerId { get; set; }
        [Column("task_id")]
        public int TaskId { get; set; }
        [Column("content")]
        public string Content { get; set; }
        [Column("description")]
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
