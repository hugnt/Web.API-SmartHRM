using HUG.CRUD.Base;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class DeductionDetailsModel : BaseModel
    {
        public Deduction DeductionDetail { get; set; }
        public Employee EmployeeDetail {  get; set; }
        public DateTime StartAt { get; set; }   
    }
}
