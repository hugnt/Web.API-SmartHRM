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
    [Table("tInsuranceDetails")]
    public class InsuranceDetails : BaseModel
    {

        [Column("insurance_id")]
        public int InsuranceId { get; set; }

        [Column("employee_id")]
        public int EmployeeId { get; set; }
        public DateTime ProvideAt { get; set; }
        public DateTime? ExpriredAt { get; set; }
        public string? ProviderAddress { get; set; }
       
    }
}
