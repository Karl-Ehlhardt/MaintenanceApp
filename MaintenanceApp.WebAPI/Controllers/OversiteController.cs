
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
    public class OversiteController : ApiController
    {
        //create service method
        private OversiteService CreateOversiteService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            OversiteService service = new OversiteService(userId);
            return service;
        }


        //=======Read======//
        //get all areas

        /// <summary>
        /// Gets  all areas
        /// </summary>
        //[HttpGet]
        //public async Task<IHttpActionResult> GetAllAreas()
        //{
        //    AreaService service = CreateAreaService();

        //    var result = await service.GetAllAreas();

        //    return Ok(result);
        //}
    }
}
