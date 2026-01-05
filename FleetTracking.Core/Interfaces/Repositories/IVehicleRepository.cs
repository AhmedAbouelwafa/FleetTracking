using FleetTracking.Core.Entites.Models;
using FleetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Core.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        // Basic CRUD
        Task<Vehicle?> GetByIdAsync(int id);
        Task<List<Vehicle>> GetAllAsync();
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(int id);

        // Real-time queries
        Task<List<Vehicle>> GetOnlineVehiclesAsync();
        Task<List<Vehicle>> GetOfflineVehiclesAsync();
        Task<List<Vehicle>> GetMovingVehiclesAsync();
        Task<List<Vehicle>> GetStoppedVehiclesAsync();

        // Advanced queries
        Task<Vehicle?> GetByPlateNumberAsync(string plateNumber);
        Task<List<Vehicle>> GetVehiclesByStatusAsync(VehicleStatus status);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();

        // Location-based queries
        Task<List<Vehicle>> GetVehiclesNearLocationAsync(
            double latitude,
            double longitude,
            double radiusKm);
    }
}
