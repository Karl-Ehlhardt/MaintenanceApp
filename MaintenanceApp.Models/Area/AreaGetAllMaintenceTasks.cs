using MaintenanceApp.Models.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Area
{
    public class AreaGetAllMaintenceTasks
    {
        public int AreaId { get; set; }

        public string AreaName { get; set; }

        public List<MachineGetAllMaintenceTasks> MachineGetAllMaintenceTasks { get; set; }

    }
}
