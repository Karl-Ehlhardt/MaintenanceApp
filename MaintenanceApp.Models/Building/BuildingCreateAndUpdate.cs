using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Models.Building
{
    public class BuildingCreateAndUpdate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name of a Building cannot be greater than 50 characters")]
        public string Name { get; set; }
    }
}
