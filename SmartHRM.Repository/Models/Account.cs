using HUG.CRUD.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Repository
{
    [Table("tAccount")]
    public class Account : BaseModel
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[]? Salt { get; set; }

    }
}
