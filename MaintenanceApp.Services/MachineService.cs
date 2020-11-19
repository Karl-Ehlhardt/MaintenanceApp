using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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

        //Create new machine
        public async Task<bool> CreateMachine(MachineCreate model)
        {
            Machine machine =
                new Machine()
                {
                    MachineName = model.MachineName
                };

            _context.Machines.Add(machine);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get all machines
        public async Task<List<MachineListItem>> GetAllMachines()
        {
            var query =
                await 
                _context
                .Machines
                .Select(
                    m =>
                    new MachineListItem()
                    {
                        MachineId = m.MachineId,
                        MachineName = m.MachineName
                    }
                    ).ToListAsync();
            return query;
        }

        //Get Machine by Id
        public async Task<List<MachineListItem>> GetMachineById(int id)
        {
            var query =
                await
                _context
                .Machines
                .Where(m => m.MachineId == id)
                .Select(
                    m =>
                    new MachineListItem()
                    {
                        MachineId = m.MachineId,
                        MachineName = m.MachineName
                    }
                    ).ToListAsync();
            return query;
        }

        //Edit Machine
        public async Task<bool> UpdateMachineById([FromUri]int id, [FromBody] MachineEdit model)
        {
            Machine machine =
                _context
                .Machines
                .Single(m => m.MachineId == id);
            machine.MachineName = model.MachineName;

            return await _context.SaveChangesAsync() == 1;
        }


        //Delete machine by id
        public async Task<bool> DeleteMachineById([FromUri] int id)
        {
            Machine machine =
                _context
                .Machines
                .Single(m => m.MachineId == id);

            _context.Machines.Remove(machine);

            return await _context.SaveChangesAsync() == 1;
        }

    }
}
