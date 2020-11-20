using MaintenanceApp.Models.Area;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Building
{
    public class BuildingGetAllMaintenceTasks
    {
        public string BuildingName { get; set; }

        public List<AreaGetAllMaintenceTasks> AreaGetAllMaintenceTasks { get; set; }
    }
}
