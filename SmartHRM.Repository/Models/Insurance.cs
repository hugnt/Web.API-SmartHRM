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
    [Table("tInsurance")]
    public class Insurance : BaseModel
    {
        public string Name { get; set; }
        public string? Benefits { get; set; }
        public string? Requirement { get; set; }
        public decimal? Amount { get; set; }

    }


}
