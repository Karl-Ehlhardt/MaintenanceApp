using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
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
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Account")]
    public class BuildingController : ApiController
    {

        private BuildingService CreateBuildingService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            BuildingService buildingService = new BuildingService(userId);
            return buildingService;
        }

        //Create
        [HttpPost]
        public async Task<IHttpActionResult> Create(BuildingCreateAndUpdate model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                BuildingService service = CreateBuildingService();

                if (await service.CreateNewBuilding(model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Building Added");
            }
        }

        //Read
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBuildings()
        {
            {
                BuildingService service = CreateBuildingService();

                //return the values as an ienumerable
                IEnumerable<BuildingListItem> buildings = await service.GetBuildings();

                return Ok(buildings);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllBuildingsById([FromUri] int id)
        {
            {
                BuildingService service = CreateBuildingService();

                //return the values as an ienumerable
                IEnumerable<BuildingListItem> building = await service.GetBuildingById(id);

                return Ok(building);
            }
        }

        [HttpGet]
        [ActionName("GetAllTasksInBuildingById")]
        public async Task<IHttpActionResult> GetAllTasksForBuildingsById([FromUri] int id)
        {
            {
                BuildingService service = CreateBuildingService();

                //return the values as an ienumerable
                IEnumerable<BuildingGetAllMaintenceTasks> building = await service.GetAllTasksInBuildingById(id);

                return Ok(building);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] BuildingCreateAndUpdate model)
        {
            {
                //check if model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //instantiate the service
                BuildingService service = CreateBuildingService();

                if (await service.UpdateBuilding(id, model) == false)
                {
                    return InternalServerError();
                }

                return Ok("Building Updated");
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            {
                BuildingService service = CreateBuildingService();

                if (await service.DeleteBuilding(id) == false)
                {
                    return InternalServerError();
                }

                return Ok($"Building Removed");
            }
        }
    }
}
