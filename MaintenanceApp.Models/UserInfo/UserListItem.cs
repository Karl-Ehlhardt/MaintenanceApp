﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.UserInfo
{
    public class UserListItem
    {
        [Key]
        public int UserInfoId { get; set; }

        public string UserName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public bool Active { get; set; }

        public DateTimeOffset InactiveDate { get; set; }
        public DateTimeOffset ReactiveDate { get; set; }
    }
}
