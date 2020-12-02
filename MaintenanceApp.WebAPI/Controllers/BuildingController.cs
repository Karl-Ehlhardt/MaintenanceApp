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
    //authorize route so only admins can do CRUD on buildings
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/Account")]
    public class BuildingController : ApiController
    {
        //building service constructor
        private BuildingService CreateBuildingService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            BuildingService buildingService = new BuildingService(userId);
            return buildingService;
        }
         
        //Create
        /// <summary>
        /// Creates a building--Enter a name in the body
        /// </summary>
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
                    return InternalServerError(); //500
                }

                return Ok("Building Added"); //200 with custom message
            }
        }

        //Read
        /// <summary>
        /// Shows all buildings
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBuildings()
        {
            {
                //instantiate building service
                BuildingService service = CreateBuildingService();

                //return the values as an ienumerable
                IEnumerable<BuildingListItem> buildings = await service.GetBuildings();

                return Ok(buildings); //200
            }
        }

        /// <summary>
        /// Gets a building by BuildingId--pass ID in the URI
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> GetBuildingsById([FromUri] int id)
        {
            {
                //instantiate service
                BuildingService service = CreateBuildingService();

                //return the values as an ienumerable
                IEnumerable<BuildingListItem> building = await service.GetBuildingById(id);

                return Ok(building); //200
            }
        }

        /// <summary>
        /// Gets all tasks for building by BuildingId--pass ID in the URI
        /// </summary>
        [HttpGet]
        [ActionName("GetAllTasksInBuildingById")]
        public async Task<IHttpActionResult> GetAllTasksForBuildingsById([FromUri] int id)
        {
            {
                //instantiate service
                BuildingService service = CreateBuildingService();

                //return the values as an ienumerable
                IEnumerable<BuildingGetAllMaintenceTasks> building = await service.GetAllTasksInBuildingById(id);

                return Ok(building);
            }
        }

        /// <summary>
        /// Updates a building by BuildingId--Enter Building Name
        /// </summary>
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

                return Ok("Building Updated"); //200 with custom message
            }
        }

        /// <summary>
        /// Update the active status of the building, all areas within that building, all machines in those areas and all of those machines MaintenanceTasks
        /// </summary>
        [HttpPut]
        [ActionName("ActiveStatus")]
        public async Task<IHttpActionResult> ActiveBuildingById([FromUri] int id)
        {
            {

                //instantiate the service
                BuildingService service = CreateBuildingService();

                if (await service.ActiveBuildingById(id) == false)
                {
                    return InternalServerError();
                }

                return Ok("Active Status Updated"); //200 with custom message
            }
        }

        //we are using the Active status to get around having to delete

        ///// <summary>
        ///// Deletes a Building--pass ID in the URI
        ///// </summary>
        //[HttpDelete]
        //public async Task<IHttpActionResult> Delete([FromUri] int id)
        //{
        //    {
        //        //instantiate service
        //        BuildingService service = CreateBuildingService();

        //        if (await service.DeleteBuilding(id) == false)
        //        {
        //            return InternalServerError();
        //        }

        //        return Ok($"Building Removed"); //200 with custom message
        //    }
        //}
    }
}
