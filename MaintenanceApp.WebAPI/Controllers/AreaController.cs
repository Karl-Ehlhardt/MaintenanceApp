using MaintenanceApp.Models.AllPurpose;
using MaintenanceApp.Models.Area;
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
    public class AreaController : ApiController
    {
        //create service method
        private AreaService CreateAreaService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            AreaService service = new AreaService(userId);
            return service;
        }

        //=======Create=====//
        /// <summary>
        /// Creates an area--Enter a name and a building Id in body.
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> CreateArea(AreaCreate area)
        {
            //check if model is valid
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AreaService service = CreateAreaService();

            if(await service.CreateArea(area) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        //=======Read======//
        //get all areas

        /// <summary>
        /// Gets  all areas
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAreas()
        {
            AreaService service = CreateAreaService();

            var result = await service.GetAllAreas();

            return Ok(result);
        }

        //get area by id

        /// <summary>
        /// Gets an area by Id--pass the Id in the URI
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetAreaById([FromUri] int id)
        {
            AreaService service = CreateAreaService();

            var result = await service.GetAreaById(id);

            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetAllTasksInAreaById")]
        /// <summary>
        /// Gets all tasks in an area by Id--pass the Id in the URI
        /// </summary>
        public async Task<IHttpActionResult> GetAllTasksInAreaById([FromUri] int id)
        {
            AreaService service = CreateAreaService();

            var result = await service.GetAllTasksInAreaById(id);

            return Ok(result);
        }

        //======Update====//
        /// <summary>
        /// Updates an area by Id--pass the Id in the URI
        /// </summary>
        [HttpPut]
        public async Task<IHttpActionResult> UpdateArea([FromUri] int id, [FromBody] AreaEdit model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AreaService service = CreateAreaService();

            if(await service.UpdateArea(id, model) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Update the active status of the area, all machines in that area and all of those machines MaintenanceTasks
        /// </summary>
        [HttpPut]
        [ActionName("ActiveStatus")]
        public async Task<IHttpActionResult> ActiveAreaById([FromUri] int id)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                AreaService service = CreateAreaService();

                if (await service.ActiveAreaById(id) == false)
                {
                    return InternalServerError();
                }

                return Ok("Active Status Updated");
            }
        }

        //=====Delete=====//
        /// <summary>
        /// Deletes an area by Id--pass the Id in the URI
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteArea([FromUri] int id)
        {
            AreaService service = CreateAreaService();

            if(await service.DeleteArea(id) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
