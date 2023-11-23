using HUG.CRUD.Base;
using SmartHRM.Repository;
using SmartHRM.Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class DepartmentDto : BaseModel
    {
        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Employee Manager { get; set; }


    }
}
