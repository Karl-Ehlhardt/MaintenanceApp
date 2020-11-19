using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using MaintenanceApp.Models.Building;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MaintenanceApp.Services
{
    public class BuildingService
    {
        private readonly Guid _userId;

        public BuildingService(Guid userId)
        {
            _userId = userId;
        }


        //create a private context
        private readonly ApplicationDbContext _context = new ApplicationDbContext();


        //==========================CREATE===============================//


        public async Task<bool> CreateNewBuilding(BuildingCreateAndUpdate model)
        {
            var entity =
                new Building()
                {
                    Name = model.Name,
                };

            _context.Buildings.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        //==========================Read===============================//


        public async Task<List<Building>> GetBuildings()
        {
            var query =
                await _context.
                Buildings.
                Select(e=>
                new Building
                {
                    BuildingId = e.BuildingId,
                    Name = e.Name
                }).ToListAsync();

            return query;
        }

        //==========================Read===============================//


        public async Task<List<Building>> GetBuildingById([FromUri] int id)
        {
            var query =
                await _context.
                Buildings.
                Where(q => q.BuildingId == id).
                Select(q =>
                new Building
                {
                    BuildingId = q.BuildingId,
                    Name = q.Name
                }).ToListAsync();

            return query;
        }

        //==========================Update===============================//


        public async Task<bool> UpdateBuilding([FromUri] int id, [FromBody] BuildingCreateAndUpdate model)
        {
            var entity =
                _context.
                Buildings.
                Single(e => e.BuildingId == id);
            entity.Name = model.Name;

            return await _context.SaveChangesAsync() == 1;
        }

        //==========================Delete===============================//


        public async Task<bool> DeleteBuilding([FromUri] int id)
        {
            var query =
                _context.
                Buildings.
                Single(q => q.BuildingId == id);

            _context.Buildings.Remove(query);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
