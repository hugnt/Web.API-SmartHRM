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
    public class AccountDto : Account
    {  
        public string? RoleName { get; set; }
  
    }
}
