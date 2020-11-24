using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.TasksForMachine
{
    public class TasksForMachineEdit
    {
        public DateTimeOffset Maintained { get; set; }
        public DateTimeOffset NeedToBeMaintainedBy { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
