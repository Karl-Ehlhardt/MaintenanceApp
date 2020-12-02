using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.TasksForMachine
{
    public class TasksForMachineCreate
    {
        [Required]
        public int MachineId { get; set; }

        public DateTimeOffset Maintained { get; set; }

        public DateTimeOffset NeedToBeMaintainedBy { get; set; }

        [Required]
        public int MaintenanceTaskId { get; set; }

        public int MaintenanceDoneById { get; set; }

        public string AssignedToId { get; set; }
    }
}
