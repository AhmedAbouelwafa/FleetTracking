using FleetTracking.Core.Entites.Models;
using FleetTracking.Core.Enums;
using FleetTracking.Core.Interfaces.Repositories;
using FleetTracking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =============== Basic CRUD ===============

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Where(v => !v.IsDeleted)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles
                .Where(v => v.IsActive && !v.IsDeleted)
                .OrderBy(v => v.PlateNumber)
                .ToListAsync();
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            vehicle.CreatedAt = DateTime.UtcNow;
            vehicle.UpdatedAt = DateTime.UtcNow;

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            vehicle.UpdatedAt = DateTime.UtcNow;

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var vehicle = await GetByIdAsync(id);
            if (vehicle != null)
            {
                vehicle.IsDeleted = true;
                vehicle.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        // =============== Real-time Queries ===============

        public async Task<List<Vehicle>> GetOnlineVehiclesAsync()
        {
            var cutoffTime = DateTime.UtcNow.AddMinutes(-10);

            return await _context.Vehicles
                .Where(v => v.IsActive
                         && !v.IsDeleted
                         && v.LastUpdatedAt != null
                         && v.LastUpdatedAt >= cutoffTime)
                .OrderByDescending(v => v.LastUpdatedAt)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetOfflineVehiclesAsync()
        {
            var cutoffTime = DateTime.UtcNow.AddMinutes(-10);

            return await _context.Vehicles
                .Where(v => v.IsActive
                         && !v.IsDeleted
                         && (v.LastUpdatedAt == null
                             || v.LastUpdatedAt < cutoffTime))
                .OrderBy(v => v.PlateNumber)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetMovingVehiclesAsync()
        {
            return await _context.Vehicles
                .Where(v => v.IsActive
                         && !v.IsDeleted
                         && v.Status == VehicleStatus.Moving)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetStoppedVehiclesAsync()
        {
            return await _context.Vehicles
                .Where(v => v.IsActive
                         && !v.IsDeleted
                         && (v.Status == VehicleStatus.Stopped
                             || v.Status == VehicleStatus.Idle))
                .ToListAsync();
        }

        // =============== Advanced Queries ===============

        public async Task<Vehicle?> GetByPlateNumberAsync(string plateNumber)
        {
            return await _context.Vehicles
                .Where(v => !v.IsDeleted)
                .FirstOrDefaultAsync(v => v.PlateNumber == plateNumber);
        }

        public async Task<List<Vehicle>> GetVehiclesByStatusAsync(VehicleStatus status)
        {
            return await _context.Vehicles
                .Where(v => v.IsActive
                         && !v.IsDeleted
                         && v.Status == status)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Vehicles
                .AnyAsync(v => v.Id == id && !v.IsDeleted);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Vehicles
                .CountAsync(v => v.IsActive && !v.IsDeleted);
        }

        // =============== Location-based Queries ===============

        public async Task<List<Vehicle>> GetVehiclesNearLocationAsync(
            double latitude,
            double longitude,
            double radiusKm)
        {
            // حساب المسافة بالـ Haversine Formula
            // Earth radius = 6371 km
            const double earthRadiusKm = 6371;

            var vehicles = await _context.Vehicles
                .Where(v => v.IsActive
                         && !v.IsDeleted
                         && v.LastLatitude != null
                         && v.LastLongitude != null)
                .ToListAsync();

            return vehicles
                .Where(v =>
                {
                    var lat1 = latitude * Math.PI / 180;
                    var lat2 = v.LastLatitude!.Value * Math.PI / 180;
                    var dLat = (v.LastLatitude.Value - latitude) * Math.PI / 180;
                    var dLon = (v.LastLongitude!.Value - longitude) * Math.PI / 180;

                    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                            Math.Cos(lat1) * Math.Cos(lat2) *
                            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

                    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                    var distance = earthRadiusKm * c;

                    return distance <= radiusKm;
                })
                .ToList();
        }
    }
}
