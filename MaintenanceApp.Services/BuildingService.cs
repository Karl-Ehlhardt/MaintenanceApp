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
                    BuildingName = model.BuildingName,
                };

            _context.Buildings.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        //============================READ===============================//


        public async Task<List<Building>> GetBuildings()
        {
            var query =
                await _context.
                Buildings.
                Select(e=>
                new Building
                {
                    BuildingId = e.BuildingId,
                    BuildingName = e.BuildingName
                }).ToListAsync();

            return query;
        }

        
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
                    BuildingName = q.BuildingName
                }).ToListAsync();

            return query;
        }

        //==========================UPDATE===============================//


        public async Task<bool> UpdateBuilding([FromUri] int id, [FromBody] BuildingCreateAndUpdate model)
        {
            var entity =
                _context.
                Buildings.
                Single(e => e.BuildingId == id);
            entity.BuildingName = model.BuildingName;

            return await _context.SaveChangesAsync() == 1;
        }

        //==========================DELETE===============================//


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
