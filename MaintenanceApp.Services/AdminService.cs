using MaintenanceApp.Data.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaintenanceApp.Models.Admin;
using System.Web.Http;
using MaintenanceApp.Data.MaintenanceData;
using System.Data.Entity;

namespace MaintenanceApp.Services
{
    public class AdminService
    {
        private readonly Guid _userId;

        public AdminService(Guid userId)
        {
            _userId = userId;
        }

        

        



        //create a private context
        private readonly ApplicationDbContext _context = new ApplicationDbContext();


        //==========================CREATE===============================//


        public async Task<bool> CreateNewAdmin(AdminCreate model)
        {
            var entity =
                new Admin()
                {
                    AdminName = model.AdminName,
                };

            _context.Admins.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        //============================READ===============================//


        public async Task<List<AdminListItem>> GetAdmins()
        {
            var query =
                await _context.
                Admins.
                Select(e =>
                new AdminListItem
                {
                    AdminId = e.AdminId,
                    AdminName = e.AdminName,
                    AdminEmail = e.AdminEmail
                }).ToListAsync();

            return query;
        }

        //==========================Read===============================//


        public async Task<List<AdminListItem>> GetAdminById([FromUri] int id)

        {
            var query =
                await _context.
                Admins.
                Where(q => q.AdminId == id).
                Select(q =>
                new AdminListItem
                {
                    AdminId = q.AdminId,
                    AdminName = q.AdminName
                }).ToListAsync();

            return query;
        }


        public async Task<bool> UpdateAdmin([FromUri] int id, [FromBody] AdminUpdate model)
        {
            var entity =
                _context.
                Admins.
                Single(e => e.AdminId == id);
            entity.AdminName = model.AdminName;
            entity.AdminEmail = model.AdminEmail;


            return await _context.SaveChangesAsync() == 1;
        }

        //==========================DELETE===============================//


        public async Task<bool> DeleteAdmin([FromUri] int id)
        {
            var query =
                _context.
                Admins.
                Single(q => q.AdminId == id);

            _context.Admins.Remove(query);

            return await _context.SaveChangesAsync() == 1;
        }


    }
}
