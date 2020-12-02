using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Area
{
    public class AreaListItem
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public bool AreaActive { get; set; }
        public int BuildingId { get; set; }
    }
}
