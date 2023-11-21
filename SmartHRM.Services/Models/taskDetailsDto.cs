using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services.Models
{
    public class TaskDetailsDto:TaskDetails
    {
        public string AssigneeName { get; set; }
        public string AssignerName { get; set; }
        public string TaskName { get; set; }
    }

}
