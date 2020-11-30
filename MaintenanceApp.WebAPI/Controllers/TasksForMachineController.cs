using MaintenanceApp.Models.TasksForMachine;
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
    [Authorize]

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
        [ActionName("CreateTasksForEverything")]
        public async Task<IHttpActionResult> CreateTasksForEverything()
        {
            //check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            if (await service.CreateTasksForEverything() == true)
            {
                return InternalServerError();
            }

            return Ok("All Tasks Generated");
        }

        //read
        [HttpGet]
        [ActionName("GetAllTasks")]
        public async Task<IHttpActionResult> GetAllTasksForMachine()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List < TasksForMachineListItem > tasksForMachine = await service.GetAll();

            return Ok(tasksForMachine);
        }

        [HttpGet]
        [ActionName("GetTasksbyId")]
        public async Task<IHttpActionResult> GetTasksbyId([FromUri] int id)
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> task = await service.GetById(id);

            return Ok(task);
        }

        [HttpGet]
        [ActionName("GetAllActiveTasks")]
        public async Task<IHttpActionResult> GetAllActiveTasks()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> tasksForMachine = await service.GetAllActiveTasks();

            return Ok(tasksForMachine);
        }

        [HttpGet]
        [ActionName("GetAllCompletedTasks")]
        public async Task<IHttpActionResult> GetAllCompletedTasks()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> tasksForMachine = await service.GetAllCompletedTasks();

            return Ok(tasksForMachine);
        }

        [HttpGet]
        [ActionName("GetAllActiveTasksAssignedToCurrentUser")]
        public async Task<IHttpActionResult> GetAllActiveTasksAssignedToCurrentUser()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> tasksForMachine = await service.GetAllActiveTasksAssignedToCurrentUser();

            return Ok(tasksForMachine);
        }

        [HttpGet]
        [ActionName("GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine")]
        public async Task<IHttpActionResult> GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine([FromBody] TasksForMachineSearch search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (search.SearchTerm == "Building" || search.SearchTerm == "Area" || search.SearchTerm == "Machine")
            {
                //instantiate service
                TasksForMachineService service = CreateTasksForMachineService();

                List<TasksForMachineByMachineIdLookup> tasksForMachine = await service.GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine(search);

                return Ok(tasksForMachine);

            }
            
            return BadRequest();

        }

        //update
        [HttpPut]
        public async Task<IHttpActionResult> CompleteAndGenerateNewTasksForMachineById([FromUri] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TasksForMachineService service = CreateTasksForMachineService();

            if(await service.CompleteAndGenerateNewTasksForMachineById(id) == false)
            {
                return InternalServerError();
            }

            return Ok("Task Complete and new task added");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMaintenanceTask([FromUri] int id)
        {
            {
                TasksForMachineService service = CreateTasksForMachineService();

                await service.RemoveTasksThatAreNoLongerNeeded();

                return Ok();
            }
        }

    }
}
