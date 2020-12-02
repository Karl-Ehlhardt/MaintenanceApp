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
        //create tasks for machine service method
        private TasksForMachineService CreateTasksForMachineService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            TasksForMachineService service = new TasksForMachineService(userId);
            return service;
        }

        //create

        /// <summary>
        /// Create Tasks deadlines from Maintenance Tasks list
        /// </summary>
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

            if (await service.CreateTasksForEverything() == false)
            {
                return InternalServerError();
            }

            return Ok("All Tasks Generated"); //200 with custom message
        }

        //read

        /// <summary>
        /// Get all tasks for Machines
        /// </summary>
        [HttpGet]
        [ActionName("GetAllTasks")]
        public async Task<IHttpActionResult> GetAllTasksForMachine()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List < TasksForMachineListItem > tasksForMachine = await service.GetAll();

            return Ok(tasksForMachine); //200
        }

        /// <summary>
        /// Get tasks by Id--pass Id from URI
        /// </summary>
        [HttpGet]
        [ActionName("GetTasksbyId")]
        public async Task<IHttpActionResult> GetTasksbyId([FromUri] int id)
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> task = await service.GetById(id);

            return Ok(task); //200
        }

        /// <summary>
        /// Get all active tasks
        /// </summary>
        [HttpGet]
        [ActionName("GetAllActiveTasks")]
        public async Task<IHttpActionResult> GetAllActiveTasks()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> tasksForMachine = await service.GetAllActiveTasks();

            return Ok(tasksForMachine); //200
        }

        /// <summary>
        /// Get all completed tasks
        /// </summary>
        [HttpGet]
        [ActionName("GetAllCompletedTasks")]
        public async Task<IHttpActionResult> GetAllCompletedTasks()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> tasksForMachine = await service.GetAllCompletedTasks();

            return Ok(tasksForMachine); //200
        }


        /// <summary>
        /// Get all tasks assigned to current user
        /// </summary>
        [HttpGet]
        [ActionName("GetAllActiveTasksAssignedToCurrentUser")]
        public async Task<IHttpActionResult> GetAllActiveTasksAssignedToCurrentUser()
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            List<TasksForMachineListItem> tasksForMachine = await service.GetAllActiveTasksAssignedToCurrentUser();

            return Ok(tasksForMachine); //200
        }

        /// <summary>
        /// Get all active tasks by building or area or machine--pass Id from Uri, and enter "Building", "Area", or "Machine in body.
        /// </summary>
        [HttpGet]
        [ActionName("GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine")]
        public async Task<IHttpActionResult> GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine([FromBody] TasksForMachineSearch search)
        {
            //check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (search.SearchTerm == "Building" || search.SearchTerm == "Area" || search.SearchTerm == "Machine")
            {
                //instantiate service
                TasksForMachineService service = CreateTasksForMachineService();

                List<TasksForMachineByMachineIdLookup> tasksForMachine = await service.GetAllActiveTasksThatAreUnassignedByIdForBuildingAreaOrMachine(search);

                return Ok(tasksForMachine); //200

            }
            
            return BadRequest(); //400

        }
        /// <summary>
        /// Get a specfic TasksForMachine using the machines Id
        /// </summary>
        [HttpGet]
        [ActionName("GetTasksAssignedToUserByMachineId")]
        public async Task<IHttpActionResult> GetTasksAssignedToUserByMachineId([FromUri] int id)
        {
            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService(); 

            List<TasksForMachineListItem> tasks = await service.GetTasksAssignedToUserByMachineId(id);

            return Ok(tasks); //200 
        }

        //update
        /// <summary>
        /// Mark task complete and set new turnover time for task to be completed
        /// </summary>
        [HttpPut]
        [ActionName("CompleteAndGenerateNewTasksForMachineById")]
        public async Task<IHttpActionResult> CompleteAndGenerateNewTasksForMachineById([FromUri] int id)
        {
            //check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //instantiate service
            TasksForMachineService service = CreateTasksForMachineService();

            if(await service.CompleteAndGenerateNewTasksForMachineById(id) == false)
            {
                return InternalServerError(); //500
            }

            return Ok("Task Complete and new task added"); //200 with custom message
        }

        /// <summary>
        /// Delete a task by Id--pass Id from URI
        /// </summary>
        [HttpDelete]
        [ActionName("Clean")]
        public async Task<IHttpActionResult> DeleteMaintenanceTask()
        {
            {
                //instantiate service
                TasksForMachineService service = CreateTasksForMachineService();

                await service.RemoveTasksThatAreNoLongerNeeded();

                return Ok(); //200
            }
        }


    }
}
