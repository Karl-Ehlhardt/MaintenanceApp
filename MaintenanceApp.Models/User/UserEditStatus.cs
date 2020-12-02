using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.User
{
    public class UserEditStatus
    {
        [Required]
        public string Email { get; set; }

        public bool Active { get; set; }
    }
}
