using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models;
using MaintenanceApp.Models.Building;
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
    public class MaintenanceTaskService
    {
        private readonly Guid _userId;

        public MaintenanceTaskService(Guid userId)
        {
            _userId = userId;
        }


        //create a private context
        private readonly ApplicationDbContext _context = new ApplicationDbContext();


        //==========================CREATE===============================//


        public async Task<bool> CreateMaintenanceTask(MaintenanceTaskCreate model)
        {
            var entity =
                new MaintenanceTask()
                {
                    MaintenanceTaskName = model.MaintenanceTaskName,
                    MaintenanceTaskDescription = model.MaintenanceTaskDescription,
                    Active = true,
                    MaintenanceTaskInterval = model.MaintenanceTaskInterval,
                    MachineId = model.MachineId
                };

            _context.Tasks.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<List<MaintenanceTaskListItem>> GetMaintenanceTaskById([FromUri] int id)
        {
            var query =
                await _context.
                Tasks.
                Where(q => q.MaintenanceTaskId == id).
                Select(q =>
                new MaintenanceTaskListItem
                {
                    MaintenanceTaskId = q.MaintenanceTaskId,
                    MaintenanceTaskName = q.MaintenanceTaskName,
                    MaintenanceTaskDescription = q.MaintenanceTaskDescription,
                    MaintenanceTaskActive = q.Active,
                    MaintenanceTaskInterval = q.MaintenanceTaskInterval,
                    ApplicationUserId = q.ApplicationUserId,
                    MachineId = q.MachineId
                }).ToListAsync();

            return query;
        }

        //[ActionName("GetTasksAssignedToUser")]
        public async Task<List<MaintenanceTaskListItem>> GetTasksAssignedToUser([FromBody] MaintenanceTaskAssign model)
        {
            var query =
                await _context.
                Tasks.
                Where(q => q.ApplicationUserId == model.ApplicationUserId).
                Select(q =>
                new MaintenanceTaskListItem
                {
                    MaintenanceTaskId = q.MaintenanceTaskId,
                    MaintenanceTaskName = q.MaintenanceTaskName,
                    MaintenanceTaskDescription = q.MaintenanceTaskDescription,
                    MaintenanceTaskActive = q.Active,
                    MaintenanceTaskInterval = q.MaintenanceTaskInterval,
                    ApplicationUserId = q.ApplicationUserId,
                    MachineId = q.MachineId
                }).ToListAsync();

            return query;
        }

        //==========================Update===============================//

        public async Task<bool> UpdateMaintenanceTaskById([FromUri] int id, [FromBody] MaintenanceTaskUpdate model)
        {
            var entity =
                _context.
                Tasks.
                Single(e => e.MaintenanceTaskId == id);
            entity.MaintenanceTaskName = model.MaintenanceTaskName;
            entity.MaintenanceTaskDescription = model.MaintenanceTaskDescription;
            entity.MaintenanceTaskInterval = model.MaintenanceTaskInterval;
            entity.MachineId = model.MachineId;

            return await _context.SaveChangesAsync() == 1;
        }

        //[ActionName("AssignTaskById")]
        public async Task<bool> AssignMaintenanceTaskById([FromUri] int id, [FromBody] MaintenanceTaskAssign model)
        {
            var entity =
                _context.
                Tasks.
                Single(e => e.MaintenanceTaskId == id);
            entity.ApplicationUserId = model.ApplicationUserId;

            return await _context.SaveChangesAsync() == 1;
        }

        //[ActionName("ActiveStatus")]
        public async Task<bool> ActiveMaintenanceTaskById([FromUri] int id)
        {
            bool switchBool;
            var entity =
                _context.
                Tasks.
                Single(e => e.MaintenanceTaskId == id);

            if (entity.Active)
            {
                switchBool = false;
                entity.Active = switchBool;
            }
            else
            {
                switchBool = true;
                entity.Active = switchBool;
            }

            return await _context.SaveChangesAsync() == 1;
        }


        //==========================Delete===============================//


        public async Task<bool> DeleteMaintenanceTask([FromUri] int id)
        {
            var query =
                _context.
                Tasks.
                Single(q => q.MaintenanceTaskId == id);

            _context.Tasks.Remove(query);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
