using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Machine
{
    public class MachineListItem
    {
        public int MachineId { get; set; }

        public string MachineName { get; set; }
        public bool MachineActive { get; set; }

        public int AreaId { get; set; }
    }
}
