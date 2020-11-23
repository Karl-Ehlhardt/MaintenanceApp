using MaintenanceApp.Models.Admin;
using MaintenanceApp.Models.Building;
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
    public class AdminController : ApiController
    {

        private AdminService CreateAdminService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId()); 
            AdminService service = new AdminService(userId);
            return service;
        }

        //Create
        [HttpPost]
        public async Task<IHttpActionResult> Create(AdminCreate model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
               AdminService service = CreateAdminService();

                if (await service.CreateNewAdmin(model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Admin Added");
            }
        }





        [HttpPut]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] AdminUpdate model)
        {
            AdminService service = CreateAdminService();

            if (service.IsAdmin(id) == false)
            {
                return Unauthorized();
            }

            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await service.UpdateAdmin(id, model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Admin Updated");
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
           
            {
               AdminService service = CreateAdminService();

                if (await service.DeleteAdmin(id) == false)
                {
                    return InternalServerError();
                }

                return Ok($"Admin Removed");
            }
        }
    }
}
