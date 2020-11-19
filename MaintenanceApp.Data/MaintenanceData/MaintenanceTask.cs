﻿using MaintenanceApp.Data.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Data.MaintenanceData
{
    public class MaintenanceTask
    {
        [Key]
        public int MaintenanceTaskId { get; set; }

        [Required]
        public string MaintenanceTaskName { get; set; }

        [Required]
        public string MaintenanceTaskDescription { get; set; }

        [Required]
        public TimeSpan MaintenanceTaskInterval { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUserDisplay { get; set; }

        [Required]
        [ForeignKey(nameof(Machine))]
        public int MachineId { get; set; }

    }
}
