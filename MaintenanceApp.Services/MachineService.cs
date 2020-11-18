using MaintenanceApp.Data.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceApp.Services
{
    public class MachineService
    {
        //private user field
        private readonly Guid _userId;

        //private appcontext
        private ApplicationDbContext _context = new ApplicationDbContext();

        //constructor
        public MachineService(Guid userId)
        {
            userId = _userId;
        }

    }
}
