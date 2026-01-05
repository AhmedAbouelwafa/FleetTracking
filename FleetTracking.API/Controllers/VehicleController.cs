using FleetTracking.Application.Contracts;
using FleetTracking.Application.Features.Vehicle.Requests;
using FleetTracking.Application.ResponseHandler;
using FleetTracking.Core.Entites.Models;
using FleetTracking.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Vehicle>>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return ApiResponseHandler.Success(vehicles, "Vehicles retrieved successfully");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Vehicle>>> GetById(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return ApiResponseHandler.NotFound<Vehicle>("Vehicle not found");
            }
            return ApiResponseHandler.Success(vehicle, "Vehicle retrieved successfully");
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Vehicle>>> Create(CreateVehicleRequest request)
        {
            var createdVehicle = await _vehicleService.AddAsync(request);
            return ApiResponseHandler.Created(createdVehicle, "Vehicle created successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Vehicle>>> Update(int id, UpdateVehicleRequest request)
        {
            var existingVehicle = await _vehicleService.ExistsAsync(id);
            if (!existingVehicle)
            {
                return ApiResponseHandler.NotFound<Vehicle>("Vehicle not found");
            }

            await _vehicleService.UpdateAsync(id, request);
            var updatedVehicle = await _vehicleService.GetByIdAsync(id);

            return ApiResponseHandler.Success(updatedVehicle!, "Vehicle updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
        {
            var existingVehicle = await _vehicleService.ExistsAsync(id);
            if (!existingVehicle)
            {
                return ApiResponseHandler.NotFound<object>("Vehicle not found");
            }

            await _vehicleService.DeleteAsync(id);
            return ApiResponseHandler.Deleted<object>("Vehicle deleted successfully");
        }

        [HttpGet("online")]
        public async Task<ActionResult<ApiResponse<List<Vehicle>>>> GetOnlineVehicles()
        {
            var vehicles = await _vehicleService.GetOnlineVehiclesAsync();
            return ApiResponseHandler.Success(vehicles, "Online vehicles retrieved successfully");
        }

        [HttpGet("offline")]
        public async Task<ActionResult<ApiResponse<List<Vehicle>>>> GetOfflineVehicles()
        {
            var vehicles = await _vehicleService.GetOfflineVehiclesAsync();
            return ApiResponseHandler.Success(vehicles, "Offline vehicles retrieved successfully");
        }

        [HttpGet("moving")]
        public async Task<ActionResult<ApiResponse<List<Vehicle>>>> GetMovingVehicles()
        {
            var vehicles = await _vehicleService.GetMovingVehiclesAsync();
            return ApiResponseHandler.Success(vehicles, "Moving vehicles retrieved successfully");
        }

        [HttpGet("stopped")]
        public async Task<ActionResult<ApiResponse<List<Vehicle>>>> GetStoppedVehicles()
        {
            var vehicles = await _vehicleService.GetStoppedVehiclesAsync();
            return ApiResponseHandler.Success(vehicles, "Stopped/Idle vehicles retrieved successfully");
        }

        [HttpGet("by-plate/{plateNumber}")]
        public async Task<ActionResult<ApiResponse<Vehicle>>> GetByPlateNumber(string plateNumber)
        {
            var vehicle = await _vehicleService.GetByPlateNumberAsync(plateNumber);
            if (vehicle == null)
            {
                return ApiResponseHandler.NotFound<Vehicle>("Vehicle not found");
            }
            return ApiResponseHandler.Success(vehicle, "Vehicle retrieved successfully");
        }

        [HttpGet("nearby")]
        public async Task<ActionResult<ApiResponse<List<Vehicle>>>> GetVehiclesNearLocation([FromQuery] double lat, [FromQuery] double lng, [FromQuery] double radiusKm = 10)
        {
            var vehicles = await _vehicleService.GetVehiclesNearLocationAsync(lat, lng, radiusKm);
            return ApiResponseHandler.Success(vehicles, "Nearby vehicles retrieved successfully");
        }
    }
}
