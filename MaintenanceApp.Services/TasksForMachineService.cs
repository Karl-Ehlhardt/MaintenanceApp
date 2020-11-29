using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models.Machine;
using MaintenanceApp.Models.TasksForMachine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MaintenanceApp.Services
{
    public class TasksForMachineService
    {
        //private user id
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //constructor
        public TasksForMachineService(Guid userId)
        {
            userId = _userId;
        }

        //============CREATE==============//
        //Create all tasks TESTING
        //[ActionName("CreateTasksForEverything")]
        public async Task<bool> CreateTasksForEverything()
        {
            foreach (MaintenanceTask task in _context.Tasks)
            {
                TasksForMachine taskMachine =
                    new TasksForMachine()
                    {
                        MachineId = task.MachineId,
                        NeedToBeMaintainedBy = DateTimeOffset.Now + task.MaintenanceTaskInterval,
                        MaintenanceTaskId = task.MaintenanceTaskId, 
                        ApplicationUserId = task.ApplicationUserId
                    };

                _context.TasksForMachines.Add(taskMachine);

            }

            return await _context.SaveChangesAsync() == 1;
        }

        //=================READ====================//
        //Get all TasksForMachines
        public async Task<List<TasksForMachineListItem>> GetAll()
        {
            var query =
                await
                _context
                .TasksForMachines
                .Select(
                    tm =>
                    new TasksForMachineListItem()
                    {
                        Id = tm.Id,
                        MachineId = tm.MachineId,
                        Maintained = tm.Maintained,
                        NeedToBeMaintainedBy = tm.NeedToBeMaintainedBy,
                        MaintenanceTaskId = tm.MaintenanceTaskId,
                        ApplicationUserId = tm.ApplicationUserId
                    }).ToListAsync();

            return query;
        }

        //Get TasksForMachine by Id
        public async Task<List<TasksForMachineListItem>> GetById(int id)
        {
            var query =
                await
                _context
                .TasksForMachines
                .Where(tm => tm.Id == id)
                .Select(
                    tm =>
                    new TasksForMachineListItem()
                    {
                        Id = tm.Id,
                        MachineId = tm.MachineId,
                        Maintained = tm.Maintained,
                        NeedToBeMaintainedBy = tm.NeedToBeMaintainedBy,
                        MaintenanceTaskId = tm.MaintenanceTaskId,
                        ApplicationUserId = tm.ApplicationUserId
                    }
                    ).ToListAsync();

            return query;
        }

        //[ActionName("GetAllActiveTasks")]
        public async Task<List<TasksForMachineListItem>> GetAllActiveTasks()
        {
            var query =
                await
                _context
                .TasksForMachines
                .Where(tm => tm.Maintained == DateTimeOffset.MinValue)
                .Select(
                    tm =>
                    new TasksForMachineListItem()
                    {
                        Id = tm.Id,
                        MachineId = tm.MachineId,
                        Maintained = tm.Maintained,
                        NeedToBeMaintainedBy = tm.NeedToBeMaintainedBy,
                        MaintenanceTaskId = tm.MaintenanceTaskId,
                        ApplicationUserId = tm.ApplicationUserId
                    }).ToListAsync();

            return query;
        }

        //[ActionName("GetAllCompletedTasks")]
        public async Task<List<TasksForMachineListItem>> GetAllCompletedTasks()
        {
            var query =
                await
                _context
                .TasksForMachines
                .Where(tm => tm.Maintained != null)
                .Select(
                    tm =>
                    new TasksForMachineListItem()
                    {
                        Id = tm.Id,
                        MachineId = tm.MachineId,
                        Maintained = tm.Maintained,
                        NeedToBeMaintainedBy = tm.NeedToBeMaintainedBy,
                        MaintenanceTaskId = tm.MaintenanceTaskId,
                        ApplicationUserId = tm.ApplicationUserId
                    }).ToListAsync();

            return query;
        }

        //[ActionName("GetAllActiveTasksAssignedToCurrentUser")]
        public async Task<List<TasksForMachineListItem>> GetAllActiveTasksAssignedToCurrentUser()
        {
            List<TasksForMachineListItem> query =
                await
                _context
                .TasksForMachines
                .Where(tm => tm.Maintained == null && tm.ApplicationUserId == _userId.ToString())
                .Select(
                    tm =>
                    new TasksForMachineListItem()
                    {
                        Id = tm.Id,
                        MachineId = tm.MachineId,
                        Maintained = tm.Maintained,
                        NeedToBeMaintainedBy = tm.NeedToBeMaintainedBy,
                        MaintenanceTaskId = tm.MaintenanceTaskId,
                        ApplicationUserId = tm.ApplicationUserId
                    }).ToListAsync();

            var sortedByDates = query.OrderBy(x => x.NeedToBeMaintainedBy).ToList();

            return sortedByDates;
        }

        //[ActionName("GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine")]
        public async Task<List<TasksForMachineByMachineIdLookup>> GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine([FromUri] int id, [FromBody] string search)
        {
            List<int> machineList = new List<int>();
            switch (search)
            {
                case "Building":
                    List<int> areaInBuildingList =
                    _context
                    .Areas
                    .Where(a => a.BuildingId == id)
                    .Select(
                    a => a.AreaId).ToList();

                    foreach (int areaId in areaInBuildingList)
                    {
                        List<int> machineInBuildingListStep =
                                    _context
                                    .Machines
                                    .Where(m => m.AreaId == areaId)
                                    .Select(
                                        m => m.MachineId).ToList();
                        foreach (int machineId in machineInBuildingListStep)
                        {
                            machineList.Add(machineId);
                        }
                    }
                    break;

                case "Area":
                    List<int> machineInAreaListStep =
                                _context
                                .Machines
                                .Where(m => m.AreaId == id)
                                .Select(
                                m => m.MachineId).ToList();
                    foreach (int machineId in machineInAreaListStep)
                    {
                        machineList.Add(machineId);
                    }
                    break;

                case "Machine":
                    machineList.Add(id);
                    break;

                default:
                    break;
            }

            List<TasksForMachineByMachineIdLookup> finalList = new List<TasksForMachineByMachineIdLookup>();

            foreach (int machineId in machineList)
            {
                var queryMidStep =
                    await
                                _context.
                                Machines.
                                Where(m => m.MachineId == machineId).
                                Select(m =>
                                new TasksForMachineByMachineIdLookup
                                {
                                    MachineId = m.MachineId,
                                    MachineName = m.MachineName,
                                    TasksForMachineListItem = 
                                        _context.
                                        TasksForMachines.
                                        Where(tm => tm.MachineId == m.MachineId && tm.Maintained == null && tm.ApplicationUserId == null).
                                        Select(tm =>
                                        new TasksForMachineListItem
                                        {
                                            Id = tm.MaintenanceTaskId,
                                            MachineId = tm.MachineId,
                                            Maintained = tm.Maintained,
                                            NeedToBeMaintainedBy = tm.NeedToBeMaintainedBy,
                                            MaintenanceTaskId = tm.MaintenanceTaskId,
                                            ApplicationUserId = tm.ApplicationUserId

                                        }).ToList()
                                }).ToListAsync();
                TasksForMachineByMachineIdLookup queryListItem = queryMidStep[0];
                finalList.Add(queryListItem);
            }

            return finalList;
        }

        //===========Update=============//
        public async Task<bool> CompleteAndGenerateNewTasksForMachineById([FromUri] int id)
        {
            var entity =
                _context
                .TasksForMachines
                .Single(tm => tm.Id == id);

            entity.Maintained = DateTime.Now;
            entity.ApplicationUserId = _userId.ToString();

            MaintenanceTask refrence =
                _context
                .Tasks
                .Single(t => t.MaintenanceTaskId == id);

            TasksForMachine newTaskMachine = new TasksForMachine()
            {
                MachineId = refrence.MachineId,
                NeedToBeMaintainedBy = DateTimeOffset.Now + refrence.MaintenanceTaskInterval,
                MaintenanceTaskId = refrence.MaintenanceTaskId,
                ApplicationUserId = refrence.ApplicationUserId
            };

            _context.TasksForMachines.Add(newTaskMachine);

            return await _context.SaveChangesAsync() == 1;
        }

        //User can mark a task complete on the machine
        public async Task<bool> UpdateTaskCompleteById([FromUri] int id)
        {

            //get task for machine based on id
            var entity =
                _context
                .TasksForMachines
                .Single(tm => tm.Id == id);

            //create new task for machine based on previous one's history
            TasksForMachine taskMachine =
                new TasksForMachine()
                {
                    MachineId = entity.MachineId,
                    Maintained = DateTimeOffset.Now,
                    NeedToBeMaintainedBy = DateTimeOffset.Now + entity.MaintenanceTask.MaintenanceTaskInterval,
                    MaintenanceTaskId = entity.MaintenanceTaskId,
                    ApplicationUserId = entity.ApplicationUserId
                };

            //add new task to db
            _context.TasksForMachines.Add(taskMachine);

            return await _context.SaveChangesAsync() == 1;
        }

        //========Delete============//
        //None
    }
}
