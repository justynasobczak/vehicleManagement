using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagementApi.Models;
using VehicleManagementModels;

namespace VehicleManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetVehicles()
        {
            try
            {
                return Ok(await _vehicleRepository.GetVehicles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to retrive data from db");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            try
            {
                var result = await _vehicleRepository.GetVehicle(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to retrive data from db");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> AddVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return BadRequest();
                }
                var createdVehicle = await _vehicleRepository.AddVehicle(vehicle);
                return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.VehicleId }, createdVehicle);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to add data");
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Vehicle>> UpdateVehicle(int id, Vehicle vehicle)
        {
            try
            {
                if (id != vehicle.VehicleId)
                {
                    return BadRequest("Vehicle id mismatch");
                }
                var vehicleToUpdate = await _vehicleRepository.GetVehicle(id);
                if (vehicleToUpdate == null)
                {
                    return NotFound($"Vehicle with id = {id} not found");
                }
                return await _vehicleRepository.UpdateVehicle(vehicle);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to update data");
            }

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            try
            {
                var vehicleToDelete = await _vehicleRepository.GetVehicle(id);
                if (vehicleToDelete == null)
                {
                    return NotFound($"Vehicle with id = {id} not found");
                }

                return await _vehicleRepository.DeleteVehicle(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Problem to date data");
            }

        }
    }
}
