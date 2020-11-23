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
            AdminService adminService = new AdminService(userId);
            return adminService;
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

        //Read
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBuildings()
        {
            {
               AdminService service = CreateAdminService();

                //return the values as an ienumerable
                IEnumerable<AdminListItem> admins = await service.GetAdmins();

                return Ok(admins);
            }
        }

        public async Task<IHttpActionResult> GetAllBuildingsById([FromUri] int id)
        {
            {
               AdminService service = CreateAdminService();

                //return the values as an ienumerable
                IEnumerable<AdminListItem> admin = await service.GetAdminById(id);

                return Ok(admin);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] AdminUpdate model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
               AdminService service = CreateAdminService();

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
