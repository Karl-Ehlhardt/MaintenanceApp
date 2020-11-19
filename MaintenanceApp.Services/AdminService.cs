using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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





        //==========================GET ALL INGREDIENTS===============================//


        public async Task<List<AdminList>> GetAllAdmins()
        {
            var query =
                await
                _context
                .Admins
                .Select(
                    e =>
                    new AdminList()
                    {
                        AdminName = e.AdminName,

                    }).ToListAsync();
            return query;
        }







        //==========================GET INGREDIENT BY NAME===============================//


        public async Task<List<AdminList>> GetAdminById([FromUri] int id)
        {
            var query =
                await
                _context
                .Admins
                .Where(e => e.AdminId == id)
                .Select(
                    e =>
                    new AdminList()
                    {
                        AdminName = e.AdminName,

                    }).ToListAsync();
            return query;
        }





        //==========================UPDATE INGREDIENT BY NAME===============================//


        public async Task<bool> UpdateAdminById([FromUri] int id, [FromBody] AdminUpdate model)
        {
            var entity =
                _context
                .Admins
                .Single(e => e.AdminId == id);
            entity.AdminName = model.AdminName;

            return await _context.SaveChangesAsync() == 1;
        }




        //==========================DELETE INGREDIENT BY ID===============================//



        public bool DeletePostsById([FromUri] int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                ctx
                .Admins
                .Single(e => e.AdminId == id);
                ctx.Admins.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}


