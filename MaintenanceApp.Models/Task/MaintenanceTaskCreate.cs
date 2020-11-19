using MaintenanceApp.Data.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Task
{
    public class MaintenanceTaskCreate
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Name of Machine cannot be greater than 50 characters")]
        public string MaintenanceTaskName { get; set; }

        [Required]
        [MaxLength(400, ErrorMessage = "Name of Machine cannot be greater than 400 characters")]
        public string MaintenanceTaskDescription { get; set; }

        [Required]
        public TimeSpan MaintenanceTaskInterval { get; set; }

        [Required]
        [ForeignKey(nameof(Data.MaintenanceData.Machine))]
        public int MachineId { get; set; }
    }
}
