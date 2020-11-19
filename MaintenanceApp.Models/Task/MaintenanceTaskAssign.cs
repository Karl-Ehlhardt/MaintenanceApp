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
    public class MaintenanceTaskAssign
    {

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public int ApplicationUserId { get; set; }
    }
}
