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
    [Table("tEmployee")]
    public class Employee : BaseModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool? Gender { get; set; }
        public string? Level { get; set; }

        [Column("department_id")]
        public int? DepartmentId { get; set; }

        [Column("position_id")]
        public int? PositionId { get; set; }

        [Column("identificationCard")]
        public string? IdentificationCard { get; set; }

        [Column("coefficient_salary")]
        public double? CoefficientSalary { get; set; }

        [Column("basic_salary")]
        public decimal? BasicSalary { get; set; }
    }
}
