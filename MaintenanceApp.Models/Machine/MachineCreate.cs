using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Machine
{
    public class MachineCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name of Machine cannot be greater than 50 characters")]
        public string MachineName { get; set; }

        [Required]
        [ForeignKey(nameof(Data.MaintenanceData.Area))]
        public int AreaId { get; set; }
    }
}
