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
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAreas()
        {
            AreaService service = CreateAreaService();

            var result = await service.GetAllAreas();

            return Ok(result);
        }

        //get area by id
        [HttpGet]
        public async Task<IHttpActionResult> GetAreaById([FromUri] int id)
        {
            AreaService service = CreateAreaService();

            var result = await service.GetAreaById(id);

            return Ok(result);
        }

        //======Update====//
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

        //=====Delete=====//
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
