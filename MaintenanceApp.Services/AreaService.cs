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
    public class AreaService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public AreaService(Guid userId)
        {
            userId = _userId;
        }

        //Create new area
        public async Task<bool> CreateArea(AreaCreate model)
        {
            Area area =
                new Area()
                {
                    AreaName = model.AreaName,
                    BuildingId = model.BuildingId
                };

            _context.Areas.Add(area);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get all areas
        public async Task<List<AreaListItem>> GetAllAreas()
        {
            var query =
                await
                _context
                .Areas
                .Select(
                    a =>
                    new AreaListItem()
                    {
                        AreaId = a.AreaId,
                        AreaName = a.AreaName,
                        BuildingId = a.BuildingId
                    }).ToListAsync();
            return query;
        }

        //Get area by id
        public async Task<List<AreaListItem>> GetAreaById([FromUri] int id)
        {
            var query =
                await
                _context
                .Areas
                .Where(a => a.AreaId == id)
                .Select(
                    a =>
                    new AreaListItem()
                    {
                        AreaId = a.AreaId,
                        AreaName = a.AreaName,
                        BuildingId = a.BuildingId
                    }).ToListAsync();
            return query;
        }

        //[ActionName("GetAllTasksInAreaById")]
        public async Task<List<AreaGetAllMaintenceTasks>> GetAllTasksInAreaById([FromUri] int id)
        {
            var query =
                await _context.
                        Areas.
                        Where(a => a.AreaId == id).
                        Select(a =>
                        new AreaGetAllMaintenceTasks
                        {
                            AreaId = a.AreaId,
                            AreaName = a.AreaName,
                            MachineGetAllMaintenceTasks = _context.
                            Machines.
                            Where(m => m.AreaId == a.AreaId).
                            Select(m =>
                            new MachineGetAllMaintenceTasks
                            {
                                MachineId = m.MachineId,
                                MachineName = m.MachineName,
                                MaintenanceTaskList = _context.
                                    Tasks.
                                    Where(t => t.MachineId == m.MachineId).
                                    Select(t =>
                                    new MaintenanceTaskListItem
                                    {
                                        MaintenanceTaskId = t.MaintenanceTaskId,
                                        MaintenanceTaskName = t.MaintenanceTaskName,
                                        MaintenanceTaskDescription = t.MaintenanceTaskDescription,
                                        MaintenanceTaskInterval = t.MaintenanceTaskInterval,
                                        //ApplicationUserId = t.ApplicationUserId,
                                        MachineId = t.MachineId,
                                    }).ToList()
                            }).ToList()
                }).ToListAsync();

            return query;
        }

        //Update area by id
        public async Task<bool> UpdateArea([FromUri] int id, [FromBody] AreaEdit model)
        {
            Area area =
                _context
                .Areas
                .Single(a => a.AreaId == id);
            area.AreaName = model.AreaName;
            area.BuildingId = model.BuildingId;

            return await _context.SaveChangesAsync() == 1;
        }

        //Delete area
        public async Task<bool> DeleteArea([FromUri] int id)
        {
            Area area =
                _context
                .Areas
                .Single(a => a.AreaId == id);

            _context.Areas.Remove(area);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}
