using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.TasksForMachine
{
    public class TasksForMachineListItem
    {
        public int Id { get; set; }

        public int MachineId { get; set; }
        public DateTimeOffset Maintained { get; set; }
        public DateTimeOffset NeedToBeMaintainedBy { get; set; }
        public int MaintenanceTaskId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
