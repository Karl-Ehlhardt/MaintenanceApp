using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.TasksForMachine
{
    public class TasksForMachineByMachineIdLookup
    {

        public int MachineId { get; set; }

        public string MachineName { get; set; }

        public List<TasksForMachineListItem> TasksForMachineListItem { get; set; }
    }
}
