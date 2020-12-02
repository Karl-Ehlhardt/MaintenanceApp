using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;

using MaintenanceApp.Models.Area;
using MaintenanceApp.Models.Machine;
using MaintenanceApp.Models.Task;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MaintenanceApp.Services
{
    public class OversiteService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public OversiteService(Guid userId)
        {
            _userId = userId;
        }

        //This will only have READ statemants in it, nothing to change
        
    }
}
