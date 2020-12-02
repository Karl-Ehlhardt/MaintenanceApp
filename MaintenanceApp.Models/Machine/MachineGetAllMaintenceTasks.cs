using MaintenanceApp.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Machine
{
    public class MachineGetAllMaintenceTasks
    {

        public int MachineId { get; set; }


        public string MachineName { get; set; }

        public bool MachineActive { get; set; }

        public List<MaintenanceTaskListItem> MaintenanceTaskList { get; set; }
    }
}
