﻿using MaintenanceApp.Models.TasksForMachine;
using MaintenanceApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MaintenanceApp.WebAPI.Controllers
{
    public class TasksForMachineController : ApiController
    {
        private TasksForMachineService CreateTasksForMachineService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            TasksForMachineService service = new TasksForMachineService(userId);
            return service;
        }

        //create
        [HttpPost]
        public async Task<IHttpActionResult> CreateTasksForMachine(TasksForMachineCreate model)
        {
            //check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            if(await service.CreateTaskForMachine(model) == false)
            {
                return InternalServerError();
            }

            return Ok($"Task for Machine with Id {model.MachineId} created.");
        }

        //read
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTasksForMachine()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List < TasksForMachineListItem > tasksForMachine = await service.GetAll();

            return Ok(tasksForMachine);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTasksForMachineById([FromUri] int id)
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> task = await service.GetById(id);

            return Ok(task);
        }

        //update
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTasksForMachineById([FromUri] int id, [FromBody] TasksForMachineEdit model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TasksForMachineService service = CreateTasksForMachineService();

            if(await service.UpdateTaskForMachineById(id, model) == false)
            {
                return InternalServerError();
            }

            return Ok("Task for machine updated");
        }

        //user update task
        [HttpPut]
        [ActionName("CompleteTask")]
        public async Task<IHttpActionResult> CompleteTaskById([FromUri] int id)
        {
            TasksForMachineService service = CreateTasksForMachineService();

            if(await service.UpdateTaskCompleteById(id) == false)
            {
                return InternalServerError();
            }

            return Ok("Task completed.");
        }
    }
}
