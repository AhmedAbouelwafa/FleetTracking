using FleetTracking.Application.Contracts;
using FleetTracking.Application.Features.Vehicle.Requests;
using FleetTracking.Core.Entites.Models;
using FleetTracking.Core.Enums;
using FleetTracking.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetTracking.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleNotificationService _notificationService;

        public VehicleService(
            IVehicleRepository vehicleRepository,
            IVehicleNotificationService notificationService)
        {
            _vehicleRepository = vehicleRepository;
            _notificationService = notificationService;
        }

        // Basic CRUD
        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _vehicleRepository.GetByIdAsync(id);
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _vehicleRepository.GetAllAsync();
        }

        public async Task<Vehicle> AddAsync(CreateVehicleRequest request)
        {
            var vehicle = new Vehicle
            {
                PlateNumber = request.PlateNumber,
                DriverName = request.DriverName,
                // Defaults
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = VehicleStatus.Offline 
            };

            return await _vehicleRepository.AddAsync(vehicle);
        }

        public async Task UpdateAsync(int id, UpdateVehicleRequest request)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                // Should probably throw exception or handle gracefully, 
                // but Controller checks ExistsAsync usually. 
                // However, simpler to check here.
                return; 
            }

            if (request.PlateNumber != null) vehicle.PlateNumber = request.PlateNumber;
            if (request.DriverName != null) vehicle.DriverName = request.DriverName;
            if (request.IsActive.HasValue) vehicle.IsActive = request.IsActive.Value;
            
            // Assume if this update logic is extended for location updates later, 
            // we'd map those here too.

            vehicle.UpdatedAt = DateTime.UtcNow;

            await _vehicleRepository.UpdateAsync(vehicle);

            // Notify via SignalR
            await _notificationService.NotifyVehicleUpdated(vehicle);
        }

        public async Task DeleteAsync(int id)
        {
            await _vehicleRepository.DeleteAsync(id);
        }

        // Real-time Queries
        public async Task<List<Vehicle>> GetOnlineVehiclesAsync()
        {
            return await _vehicleRepository.GetOnlineVehiclesAsync();
        }

        public async Task<List<Vehicle>> GetOfflineVehiclesAsync()
        {
            return await _vehicleRepository.GetOfflineVehiclesAsync();
        }

        public async Task<List<Vehicle>> GetMovingVehiclesAsync()
        {
            return await _vehicleRepository.GetMovingVehiclesAsync();
        }

        public async Task<List<Vehicle>> GetStoppedVehiclesAsync()
        {
            return await _vehicleRepository.GetStoppedVehiclesAsync();
        }

        // Advanced Queries
        public async Task<Vehicle?> GetByPlateNumberAsync(string plateNumber)
        {
            return await _vehicleRepository.GetByPlateNumberAsync(plateNumber);
        }

        public async Task<List<Vehicle>> GetVehiclesByStatusAsync(VehicleStatus status)
        {
            return await _vehicleRepository.GetVehiclesByStatusAsync(status);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _vehicleRepository.ExistsAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _vehicleRepository.GetTotalCountAsync();
        }

        // Location-based Queries
        public async Task<List<Vehicle>> GetVehiclesNearLocationAsync(double latitude, double longitude, double radiusKm)
        {
            return await _vehicleRepository.GetVehiclesNearLocationAsync(latitude, longitude, radiusKm);
        }
    }
}
