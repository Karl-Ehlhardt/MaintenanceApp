using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Area
{
    public class AreaCreate
    {
        [Required]
        [MaxLength(40, ErrorMessage = "Area Name cannot be greater than 40 characters.")]
        public string AreaName { get; set; }
    }
}
