using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models.AllPurpose;
using MaintenanceApp.Models.Building;
using MaintenanceApp.Models.Task;
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
    public class MaintenanceTaskController : ApiController
    {

        private MaintenanceTaskService CreateMaintenanceTaskService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            MaintenanceTaskService maintenanceTaskService = new MaintenanceTaskService(userId);
            return maintenanceTaskService;
        }

        //Create
        /// <summary>
        /// Creates a Maintenance Task
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> Create(MaintenanceTaskCreate model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                if (await service.CreateMaintenanceTask(model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Task Added");
            }
        }

        //Read
        [HttpGet]
        /// <summary>
        /// Gets a Maintenance Task by Id--pass the Id in the URI
        /// </summary>
        public async Task<IHttpActionResult> GetMaintenanceTaskById([FromUri] int id)
        {
            {
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                //return the values as an ienumerable
                IEnumerable<MaintenanceTaskListItem> task = await service.GetMaintenanceTaskById(id);

                return Ok(task);
            }
        }
        
        /// <summary>
        /// Gets Maintenance Tasks assigned to a user--Enter a user string in the body
        /// </summary>
        [HttpGet]
        [ActionName("GetTasksAssignedToUser")]
        public async Task<IHttpActionResult> GetTasksAssignedToUser([FromBody] MaintenanceTaskAssign model)
        {
            {
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                //return the values as an ienumerable
                IEnumerable<MaintenanceTaskListItem> task = await service.GetTasksAssignedToUser(model);

                return Ok(task);
            }
        }

        /// <summary>
        /// Update Maintenance Tasks by Id--pass Id from URI
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> UpdateMaintenanceTaskById([FromUri] int id, [FromBody] MaintenanceTaskUpdate model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                if (await service.UpdateMaintenanceTaskById(id, model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Task Updated");
            }
        }

        /// <summary>
        /// Assign Maintenance Tasks by Id--pass Id from URI
        /// </summary>
        [HttpPut]
        [ActionName("AssignTaskById")]
        public async Task<IHttpActionResult> AssignMaintenanceTaskById([FromUri] int id, [FromBody] MaintenanceTaskAssign model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                if (await service.AssignMaintenanceTaskById(id, model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Task Assigned");
            }
        }


        /// <summary>
        /// Update the active status of the MaintenanceTask
        /// </summary>
        [HttpPut]
        [ActionName("ActiveStatus")]
        public async Task<IHttpActionResult> ActiveMaintenanceTaskById([FromUri] int id)
        {
            {

                //instantiate the service
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                if (await service.ActiveMaintenanceTaskById(id) == false)
                {
                    return InternalServerError();
                }

                return Ok("Active Status Updated");
            }
        }


        /// <summary>
        /// Delete Maintenance Tasks by Id--pass Id from URI
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMaintenanceTask([FromUri] int id)
        {
            {
                MaintenanceTaskService service = CreateMaintenanceTaskService();

                if (await service.DeleteMaintenanceTask(id) == false)
                {
                    return InternalServerError();
                }

                return Ok("Task Removed");
            }
        }
    }
}
