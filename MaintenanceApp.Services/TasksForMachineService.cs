using _00_HelperMethods;
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
            _userId = userId;
        }

        //============CREATE==============//
        //Create all tasks TESTING
        //[ActionName("CreateTasksForEverything")]
        public async Task<bool> CreateTasksForEverything()
        {
            List<int> currentTasksForMachine = new List<int>();

            bool okToAdd = true;

            foreach (TasksForMachine existing in _context.TasksForMachines)
            {
                currentTasksForMachine.Add(existing.MaintenanceTaskId);
            }

            foreach (MaintenanceTask task in _context.Tasks)
            {
                foreach (int existingId in currentTasksForMachine)
                {
                    if (existingId == task.MaintenanceTaskId)
                    {
                        okToAdd = false;
                        break;
                    }
                    else if(task.Active == false)
                    {
                        okToAdd = false;
                        break;
                    }
                }

                if (okToAdd)
                {
                    //takes in a datetimeoffset and converts it to weekday
                    DateTimeOffset dateTimeOffset = DateTimeOffset.Now + TimeSpan.FromTicks(task.MaintenanceTaskInterval);
                    Methods helperMethod = new Methods();

                    DateTimeOffset modifiedDate = helperMethod.ConvertToDayOfWeek(dateTimeOffset);

                    //takes in a datetimeoffset and converts only the time to 5:00pm
                    DateTimeOffset modifiedTime = helperMethod.ConvertToEndOfDayTime(modifiedDate);

                    TasksForMachine taskMachine =
                    new TasksForMachine()
                    {
                        MachineId = task.MachineId,
                        NeedToBeMaintainedBy = modifiedTime,
                        MaintenanceTaskId = task.MaintenanceTaskId,
                        ApplicationUserId = task.ApplicationUserId
                    };

                    _context.TasksForMachines.Add(taskMachine);
                }

                okToAdd = true;
            }

            //Remove the below comments when test is passed

            //foreach (MaintenanceTask task in _context.Tasks)
            //{
            //    bool okToAdd = true;
            //    TasksForMachine taskMachine =
            //        new TasksForMachine()
            //        {
            //            MachineId = task.MachineId,
            //            NeedToBeMaintainedBy = DateTimeOffset.Now + task.MaintenanceTaskInterval,
            //            MaintenanceTaskId = task.MaintenanceTaskId, 
            //            ApplicationUserId = task.ApplicationUserId
            //        };

            //    foreach (TasksForMachine existing in _context.TasksForMachines)
            //    {
            //        if (existing.MaintenanceTaskId == taskMachine.MaintenanceTaskId)
            //        {
            //            okToAdd = false;
            //            break;
            //        }
            //    }

            //    if (okToAdd)
            //    {
            //    _context.TasksForMachines.Add(taskMachine);
            //    }
            //}

            return await _context.SaveChangesAsync() >= 1;
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
                .Where(tm => tm.Maintained != DateTimeOffset.MinValue)
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
                .Where(tm => tm.Maintained == DateTimeOffset.MinValue && tm.ApplicationUserId == _userId.ToString())
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
        public async Task<List<TasksForMachineByMachineIdLookup>> GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine([FromBody] TasksForMachineSearch search)
        {
            List<int> machineList = new List<int>();
            switch (search.SearchTerm)
            {
                case "Building":
                    List<int> areaInBuildingList =
                    _context
                    .Areas
                    .Where(a => a.BuildingId == search.SearchId)
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
                                .Where(m => m.AreaId == search.SearchId)
                                .Select(
                                m => m.MachineId).ToList();
                    foreach (int machineId in machineInAreaListStep)
                    {
                        machineList.Add(machineId);
                    }
                    break;

                case "Machine":
                    machineList.Add(search.SearchId);
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
                                        Where(tm => tm.MachineId == m.MachineId && tm.Maintained == DateTimeOffset.MinValue && tm.ApplicationUserId == null).
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

        public async Task<List<TasksForMachineListItem>> GetTasksAssignedToUserByMachineId([FromUri] int id)
        {
            var task =
                await
                _context
                .TasksForMachines
                .Where(t => t.MachineId == id & t.ApplicationUserId == _userId.ToString())
                .Select(
                    t => new TasksForMachineListItem()
                    {
                        Id = t.Id,
                        MachineId = t.MachineId,
                        Maintained = t.Maintained,
                        NeedToBeMaintainedBy = t.NeedToBeMaintainedBy,
                        MaintenanceTaskId = t.MaintenanceTaskId,
                        ApplicationUserId = t.ApplicationUserId
                    }
                    ).ToListAsync();
            return task;
        }

        //===========Update=============//
        public async Task<bool> CompleteAndGenerateNewTasksForMachineById([FromUri] int id)
        {
            var entity =
                _context
                .TasksForMachines
                .SingleOrDefault(tm => tm.Id == id);

            entity.Maintained = DateTime.Now;
            entity.ApplicationUserId = _userId.ToString();

            MaintenanceTask refrence =
                _context
                .Tasks
                .SingleOrDefault(t => t.MaintenanceTaskId == entity.MaintenanceTaskId);

            //get datetimeoffset and modify time and day to be on a weekday at 5:00pm for need to be maintained by prop
            //takes in a datetimeoffset and converts it to weekday
            DateTimeOffset dateTimeOffset = DateTimeOffset.Now + TimeSpan.FromTicks(refrence.MaintenanceTaskInterval);
            Methods helperMethod = new Methods();

            DateTimeOffset modifiedDate = helperMethod.ConvertToDayOfWeek(dateTimeOffset);

            //takes in a datetimeoffset and converts only the time to 5:00pm
            DateTimeOffset modifiedTime = helperMethod.ConvertToEndOfDayTime(modifiedDate);


            TasksForMachine newTaskMachine = new TasksForMachine()
            {
                MachineId = refrence.MachineId,
                NeedToBeMaintainedBy = modifiedTime,
                MaintenanceTaskId = refrence.MaintenanceTaskId,
                ApplicationUserId = refrence.ApplicationUserId
            };

            _context.TasksForMachines.Add(newTaskMachine);

            return await _context.SaveChangesAsync() >= 1;
        }

        //========Delete============//
        //[ActionName("RemoveExtra")]
        public async Task<bool> RemoveTasksThatAreNoLongerNeeded()
        {
            //Remove comented section below if this runs
            List<int> activeMaintenanceTasks = new List<int>();

            bool okToRemove = true;

            foreach (MaintenanceTask existing in _context.Tasks)
            {
                if (existing.Active == true)
                {
                    activeMaintenanceTasks.Add(existing.MaintenanceTaskId);
                }
            }

            foreach (TasksForMachine task in _context.TasksForMachines)
            {
                foreach (int existingId in activeMaintenanceTasks)
                {
                    if(task.Maintained > DateTimeOffset.MinValue)
                    {
                        okToRemove = false;
                        break;
                    }

                    else if (existingId == task.MaintenanceTaskId)
                    {
                        okToRemove = false;
                        break;
                    }
                }

                if (okToRemove)
                {
                    _context.TasksForMachines.Remove(task);
                }
                okToRemove = true;
            }

            //foreach (MaintenanceTask needsToExist in _context.Tasks)
            //{
            //    bool okToRemove = true;
            //    TasksForMachine currentQuery = new TasksForMachine();

            //    foreach (TasksForMachine existing in _context.TasksForMachines)
            //    {
            //        currentQuery = existing;
            //        if (existing.MaintenanceTaskId == needsToExist.MaintenanceTaskId && existing.Maintained == DateTimeOffset.MinValue)
            //        {
            //            okToRemove = false;
            //            break;
            //        }
            //    }

            //    if (okToRemove)
            //    {
            //        _context.TasksForMachines.Remove(currentQuery);
            //    }
            //    okToRemove = true;
            //}

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
