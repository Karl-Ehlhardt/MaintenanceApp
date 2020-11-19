﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Data.MaintenanceData
{
    public class Area
    {
        [Key]
        public int AreaId { get; set; }

        [Required]
        public string AreaName { get; set; }

        [Required]
        [ForeignKey(nameof(Building))]
        public int BuildingId { get; set; }
    }
}
