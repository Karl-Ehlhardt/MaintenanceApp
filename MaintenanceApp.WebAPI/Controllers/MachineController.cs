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
        [HttpPost]
        public async Task<IHttpActionResult> CreateMachine(MachineCreate machine)
        {
            //Check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //404
            }

            //Instantiate service
            MachineService service = CreateMachineService();

            if(await service.CreateMachine(machine) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        //=======Read======//
        //get all machines
        [HttpGet]
        public async Task<IHttpActionResult> GetMachines()
        {
            //instantiate service
            MachineService service = CreateMachineService();

            List<MachineListItem> machines = await service.GetAllMachines();

            return Ok(machines);
        }

        //get machines by id
        [HttpGet]
        public async Task<IHttpActionResult> GetMachineById([FromUri] int id)
        {
            //instantiate service
            MachineService service = CreateMachineService();

            List<MachineListItem> machine = await service.GetMachineById(id);

            return Ok(machine);
        }


        [HttpGet]
        [ActionName("GetAllTasksForMachineById")]
        public async Task<IHttpActionResult> GetAllTasksForMachineById([FromUri] int id)
        {
            MachineService service = CreateMachineService();

            var result = await service.GetAllTasksForMachineById(id);

            return Ok(result);
        }

        //=====Update=====//
        [HttpPut]
        public async Task<IHttpActionResult> UpdateMachine([FromUri] int id, [FromBody] MachineEdit model)
        {
            //check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); //404
            }

            //instantiate service
            MachineService service = CreateMachineService();

            //check if updated
            if(await service.UpdateMachineById(id, model) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

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

        //======Delete=====//
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMachine([FromUri] int id)
        {
            //instantiate service
            MachineService service = CreateMachineService();

            if(await service.DeleteMachineById(id) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
