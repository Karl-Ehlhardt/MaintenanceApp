using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MaintenanceApp.Services
{
    public class UserInfoService
    {
        //private user id
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //constructor
        public UserInfoService(Guid userid)
        {
            userid = _userId;
        }

        //===========CREATE=============//
        //create new user
        public async Task<bool> CreateUser(UserInfoCreate model)
        {
            UserInfo user =
                new UserInfo()
                {
                    UserName = model.UserName,
                    StartDate = model.StartDate,
                    Active = model.Active
                };

            _context.UserInformation.Add(user);

            return await _context.SaveChangesAsync() == 1;
        }

        //==============READ=============//
        //Get all users
        public async Task<List<UserInfoListItem>> GetAllUsers()
        {
            var query =
                await
                _context
                .UserInformation
                .Select(
                    ui =>
                    new UserInfoListItem()
                    {
                        UserInfoId = ui.UserInfoId,
                        UserName = ui.UserName,
                        StartDate = ui.StartDate,
                        Active = ui.Active,
                        InactiveDate = ui.InactiveDate,
                        ReactiveDate = ui.ReactiveDate
                    }
                    ).ToListAsync();

            return query;
        }

        //Get user by id
        public async Task<List<UserInfoListItem>> GetUserById([FromUri] int id)
        {
            var query =
                await
                _context
                .UserInformation
                .Where(ui => ui.UserInfoId == id)
                .Select(
                    ui =>
                    new UserInfoListItem()
                    {
                        UserName = ui.UserName,
                        StartDate = ui.StartDate,
                        Active = ui.Active,
                        InactiveDate = ui.InactiveDate,
                        ReactiveDate = ui.ReactiveDate
                    }).ToListAsync();
            return query;
        }

        //==================Update=============//
    }
}
