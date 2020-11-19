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
    public class MaintenanceTaskListItem
    {

        public int MaintenanceTaskId { get; set; }

        public string MaintenanceTaskName { get; set; }

        public string MaintenanceTaskDescription { get; set; }

        public TimeSpan MaintenanceTaskInterval { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public int ApplicationUserId { get; set; }
    }
}
