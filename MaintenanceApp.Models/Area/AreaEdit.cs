using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Area
{
    public class AreaEdit
    {
        [Required]
        public int AreaId { get; set; }

        [Required]
        public string AreaName { get; set; }

        [Required]
        [ForeignKey(nameof(Data.MaintenanceData.Building))]
        public int BuildingId { get; set; }
    }
}
