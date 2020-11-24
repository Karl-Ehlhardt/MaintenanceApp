using MaintenanceApp.Data.UserData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Data.MaintenanceData
{
    public class TasksForMachine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MachineId { get; set; }

        [ForeignKey(nameof(MachineId))]
        public virtual Machine Machine { get; set; }

        public DateTimeOffset Maintained { get; set; }

        [Required]
        public DateTimeOffset NeedToBeMaintainedBy { get; set; }

        [Required]
        public int MaintenanceTaskId { get; set; }

        [ForeignKey(nameof(MaintenanceTaskId))]
        public virtual MaintenanceTask MaintenanceTask { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUserDisplay { get; set; }

        //[Required]
        //public int AssignedToId { get; set; }

        //[ForeignKey(nameof(UserInfo))]
        //public virtual UserInfo UserInfo { get; set; }
    }
}
