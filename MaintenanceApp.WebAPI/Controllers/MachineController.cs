using MaintenanceApp.Models;
using MaintenanceApp.Models.Machine;
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
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Account")]
    public class MachineController : ApiController
    {
        //initiate machine service
        private MachineService CreateMachineService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            MachineService service = new MachineService(userId);
            return service;
        }

        //=====Create=====//

        /// <summary>
        /// Creates a Machine--Enter an Area Id
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> CreateMachine(MachineCreate machine)
        {
            //Check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //404
            }

            //Instantiate service
            MachineService service = CreateMachineService();

            if (await service.CreateMachine(machine) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        //=======Read======//
        //get all machines

        /// <summary>
        /// Gets all Machines
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetMachines()
        {
            //instantiate service
            MachineService service = CreateMachineService();

            List<MachineListItem> machines = await service.GetAllMachines();

            return Ok(machines);
        }

        //get machines by id
        /// <summary>
        /// Gets a Machine by Id--Pass Id in URI
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetMachineById([FromUri] int id)
        {
            //instantiate service
            MachineService service = CreateMachineService();

            List<MachineListItem> machine = await service.GetMachineById(id);

            return Ok(machine);
        }

        /// <summary>
        /// Gets all tasks for a Machine by Id--Pass Id in URI
        /// </summary>
        [HttpGet]
        [ActionName("GetAllTasksForMachineById")]
        public async Task<IHttpActionResult> GetAllTasksForMachineById([FromUri] int id)
        {
            MachineService service = CreateMachineService();

            var result = await service.GetAllTasksForMachineById(id);

            return Ok(result);
        }

        //=====Update=====//

        /// <summary>
        /// Updates a machine by Id--pass Id in URI, enter edits in body
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> UpdateMachine([FromUri] int id, [FromBody] MachineEdit model)
        {
            //check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //404
            }

            //instantiate service
            MachineService service = CreateMachineService();

            //check if updated
            if (await service.UpdateMachineById(id, model) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }
        /// <summary>
        /// Assign all tasks for a machine by id--pass Id in URI, enter assignment in body
        /// </summary>
        [HttpPut]
        [ActionName("AssignAllTaskForMachineById")]
        public async Task<IHttpActionResult> AssignAllTaskForMachineById([FromUri] int id, [FromBody] MaintenanceTaskAssign model)
        {
            //check if model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //404
            }

            //instantiate service
            MachineService service = CreateMachineService();

            //check if updated
            if (await service.AssignAllTaskForMachineById(id, model) == false)
            {
                return InternalServerError();
            }

            return Ok("All Tasks Assigned");

        }

        /// <summary>
        /// Update the active status of the machine and all of its MaintenanceTasks
        /// </summary>
        [HttpPut]
        [ActionName("ActiveStatus")]
        public async Task<IHttpActionResult> ActiveMachineById([FromUri] int id)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                MachineService service = CreateMachineService();

                if (await service.ActiveMachineById(id) == false)
                {
                    return InternalServerError();
                }

                return Ok("Active Status Updated");
            }
        }

        //======Delete=====//

        /// <summary>
        /// Delete a Machine by Id--Pass Id in URI
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMachine([FromUri] int id)
        {
            //instantiate service
            MachineService service = CreateMachineService();

            if (await service.DeleteMachineById(id) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
