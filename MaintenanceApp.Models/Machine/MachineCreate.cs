using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models
{
    public class MachineCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name of Machine cannot be greater than 50 characters")]
        public string MachineName { get; set; }
    }
}
