using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vegga.Controllers.Resources;
using vegga.Models;
using vegga.Persistence;
using vegga.Persistence.Repositories.Contracts;
using vegga.Services;

namespace vegga.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repo;
        private readonly IUnitOfWork uow;

        public VehiclesController(IMapper mapper, IVehicleRepository repo, IUnitOfWork uow)
        {
            this.repo = repo;
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddVehicle([FromBody]SaveVehicleResource vehicleResource)
        {            
            // if(!ModelState.IsValid)
            // {
            //     this.HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            //     return Json(new {
            //         message = "Validation Error",
            //         errors = ModelState.Values.SelectMany(v => v.Errors)
            //     });
            // }

            var model = await repo.GetModel(vehicleResource.ModelId);

            if(model == null){
                ModelState.AddModelError("ModelId","Invalid ModelId");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repo.Add(vehicle);

            await uow.Complete();

            vehicle = await repo.GetVehicleAsync(vehicle.Id);

            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);

            //return Ok(result);
            return Created(Request.Path+"/"+result.Id,result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle([FromBody] SaveVehicleResource vehicleResource,int id)
        {
            var vehicle = await repo.GetVehicleAsync(id);

            if(vehicle == null)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map<SaveVehicleResource,Vehicle>(vehicleResource,vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await uow.Complete();

            vehicle = await repo.GetVehicleAsync(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repo.GetVehicleAsync(id, includeRelated: false);

            if(vehicle == null)
                return NotFound();

            repo.Remove(vehicle);

            await uow.Complete();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repo.GetVehicleAsync(id);

            if(vehicle == null)
                return NotFound();

            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);

            return Ok(result);
        }



    }
}