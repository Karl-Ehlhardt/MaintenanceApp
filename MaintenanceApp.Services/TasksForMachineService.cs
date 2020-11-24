﻿using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
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
        //Create new task for machine
        public async Task<bool> CreateTaskForMachine(TasksForMachineCreate model)
        {
            TasksForMachine taskMachine =
                new TasksForMachine()
                {
                    MachineId = model.MachineId,
                    Maintained = model.Maintained,
                    NeedToBeMaintainedBy = model.NeedToBeMaintainedBy,
                    MaintenanceTaskId = model.MaintenanceTaskId,
                    UserInfoId = model.AssignedToId
                };

            _context.TasksForMachines.Add(taskMachine);

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
                        AssignedToId = tm.UserInfoId
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
                        AssignedToId = tm.UserInfoId
                    }
                    ).ToListAsync();

            return query;
        }

        //===========Update=============//
        public async Task<bool> UpdateTaskForMachineById([FromUri]int id, [FromBody]TasksForMachineEdit model)
        {
            var entity =
                _context
                .TasksForMachines
                .Single(tm => tm.Id == id);

            entity.Maintained = model.Maintained;
            entity.NeedToBeMaintainedBy = model.NeedToBeMaintainedBy;
            entity.UserInfoId = model.AssignedToId;

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

            //change the field mantained to equal current date and time
            entity.Maintained = DateTimeOffset.Now;

            //make sure that the timespan is applied to need to be maintained by in order to get correct interval
            entity.NeedToBeMaintainedBy = DateTimeOffset.Now + entity.MaintenanceTask.MaintenanceTaskInterval;

            return await _context.SaveChangesAsync() == 1;
        }

        //========Delete============//
        public async Task<bool> DeleteById([FromUri] int id)
        {
            var entity =
                _context
                .TasksForMachines
                .Single(tm => tm.Id == id);

            _context.TasksForMachines.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
