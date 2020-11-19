using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Data.MaintenanceData
{
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }

        [Required]
        public string MachineName { get; set; }
    }
}
