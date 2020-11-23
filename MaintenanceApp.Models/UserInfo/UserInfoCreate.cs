using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.UserInfo
{
    public class UserInfoCreate
    {
        [Required]
        [MaxLength(40, ErrorMessage = "Username cannot be longer than 40 characters.")]
        public string UserName { get; set; }

        public DateTimeOffset StartDate { get; set; }
        
        [Required]
        public bool Active { get; set; }

    }
}
