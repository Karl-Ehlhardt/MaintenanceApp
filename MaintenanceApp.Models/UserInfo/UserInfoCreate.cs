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
        public string UserName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public bool Active { get; set; }

    }
}
