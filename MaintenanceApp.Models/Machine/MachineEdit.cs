using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Machine
{
    public class MachineEdit
    {
        [Required]
        public string MachineName { get; set; }

        [Required]
        [ForeignKey(nameof(Data.MaintenanceData.Area))]
        public int AreaId { get; set; }
    }
}
