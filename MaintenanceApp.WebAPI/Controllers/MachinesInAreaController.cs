using MaintenanceApp.Data.MaintenanceData;
using MaintenanceApp.Data.UserData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MaintenanceApp.WebAPI.Controllers
{
    //public class MachinesInAreaController : ApiController
    //{
    //    //private context
    //    ApplicationDbContext _context = new ApplicationDbContext();

    //    //Add a machine to an area or vice-versa
    //    [HttpPost]
    //    public async Task<IHttpActionResult> AddMachineToArea(MachinesInArea ma)
    //    {
    //        if(!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        Machine machine = await _context.Machines.FindAsync(ma.MachineId);
    //        Area area = await _context.Areas.FindAsync(ma.AreaId);

    //        if(machine == null)
    //        {
    //            return BadRequest("Machine Id not valid");
    //        }

    //        if(area == null)
    //        {
    //            return BadRequest("Area Id not valid.");
    //        }

    //        _context.MachinesInAreas.Add(ma);
    //        await _context.SaveChangesAsync();

    //        return Ok();
    //    }

    //    //Get machines in areas
    //    [HttpGet]
    //    public async Task<IHttpActionResult> GetMachinesInAllAres()
    //    {
    //        List < MachinesInArea > machinesInAreas = await _context.MachinesInAreas.ToListAsync();

    //        return Ok(machinesInAreas);
    //    }

    //    //Get machines by area id
    //    [HttpGet]
    //    public async Task<IHttpActionResult> GetMachinesByArea([FromUri] int id) //this refers to the area id
    //    {
    //        List<MachinesInArea> machinesInAreas =
    //            await
    //            _context
    //            .MachinesInAreas
    //            .Where(a => a.AreaId == id).ToListAsync();

    //        return Ok(machinesInAreas);
    //    }
    //}
}
