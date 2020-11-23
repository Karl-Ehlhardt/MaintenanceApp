using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Admin
{
    public class AdminListItem
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminEmail { get; set; }
        public string AdminName { get; set; }
    }
}
