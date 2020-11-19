using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Data.MaintenanceData
{
    public class MachinesInArea
    {
        //This will be a joining table for Machine and Area

        [Key]
        public int Id { get; set; }

        [Required]
        public int MachineId { get; set; }

        [ForeignKey(nameof(MachineId))]
        public virtual Machine Machine { get; set; }

        [Required]
        public int AreaId { get; set; }
        
        [ForeignKey(nameof(AreaId))]
        public virtual Area Area { get; set; }

    }
}
