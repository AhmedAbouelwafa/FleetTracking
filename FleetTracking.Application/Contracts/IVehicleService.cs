using FleetTracking.Core.Entites.Models;
using FleetTracking.Core.Enums;
using FleetTracking.Application.Features.Vehicle.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetTracking.Application.Contracts
{
    public interface IVehicleService
    {
        // Basic CRUD
        Task<Vehicle?> GetByIdAsync(int id);
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle> AddAsync(CreateVehicleRequest request);
        Task UpdateAsync(int id, UpdateVehicleRequest request);
        Task DeleteAsync(int id);

        // Real-time Queries
        Task<List<Vehicle>> GetOnlineVehiclesAsync();
        Task<List<Vehicle>> GetOfflineVehiclesAsync();
        Task<List<Vehicle>> GetMovingVehiclesAsync();
        Task<List<Vehicle>> GetStoppedVehiclesAsync();

        // Advanced Queries
        Task<Vehicle?> GetByPlateNumberAsync(string plateNumber);
        Task<List<Vehicle>> GetVehiclesByStatusAsync(VehicleStatus status);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();

        // Location-based Queries
        Task<List<Vehicle>> GetVehiclesNearLocationAsync(double latitude, double longitude, double radiusKm);
    }
}
