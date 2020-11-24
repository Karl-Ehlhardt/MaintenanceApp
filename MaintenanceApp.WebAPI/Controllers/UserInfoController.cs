using MaintenanceApp.Models.UserInfo;
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
    public class UserInfoController : ApiController
    {
        //create service
        private UserInfoService CreateUserInfoService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            UserInfoService service = new UserInfoService(userId);
            return service;
        }

        //Create
        [HttpPost]
        public async Task<IHttpActionResult> CreateNewUserInfo(UserInfoCreate model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //instantiate service
            UserInfoService service = CreateUserInfoService();

            if(await service.CreateUser(model) == false)
            {
                return InternalServerError();
            }

            return Ok("User created");
        }

        //Read
        [HttpGet]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            UserInfoService service = CreateUserInfoService();

            List<UserInfoListItem> users = await service.GetAllUsers();

            return Ok(users);
        }

        //get user by id
        [HttpGet]
        public async Task<IHttpActionResult> GetUserById([FromUri] int id)
        {
            UserInfoService service = CreateUserInfoService();

            List<UserInfoListItem> user = await service.GetUserById(id);

            return Ok(user);
        }

        //Admin changes user active status
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> UpdateUserActiveStatus([FromUri] int id)
        {
            UserInfoService service = CreateUserInfoService();

            if(await service.UpdateUserActiveStatus(id) == false)
            {
                return InternalServerError();
            }

            return Ok("User active status updated.");
        }
    }
}
