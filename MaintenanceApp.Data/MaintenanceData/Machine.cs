using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        [ForeignKey(nameof(Area))]
        public int AreaId { get; set; }

        public virtual Area Area { get; set; }
    }
}
