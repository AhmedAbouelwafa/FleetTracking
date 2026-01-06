using FleetTracking.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetTracking.API.Services
{
    public class SimulatedVehicle
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // Add other properties if needed based on "باقي الخصائص" (rest of properties)
    }

    public class LocationSimulatorService : BackgroundService
    {
        private readonly IHubContext<VehicleHub> _hubContext;
        private readonly List<SimulatedVehicle> _vehicles;
        private readonly Random _random = new Random();

        public LocationSimulatorService(IHubContext<VehicleHub> hubContext)
        {
            _hubContext = hubContext;
            
            // Initialize dummy vehicles
            _vehicles = new List<SimulatedVehicle>();
            for (int i = 1; i <= 5; i++)
            {
                _vehicles.Add(new SimulatedVehicle 
                { 
                    Id = i, 
                    Latitude = 24.7136 + (i * 0.01), // Near Riyadh or generic
                    Longitude = 46.6753 + (i * 0.01) 
                });
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var vehicle in _vehicles)
                {
                    // Simulate some movement
                    vehicle.Latitude += (_random.NextDouble() - 0.5) * 0.001;
                    vehicle.Longitude += (_random.NextDouble() - 0.5) * 0.001;

                    await SendVehicleUpdate(vehicle, stoppingToken);
                }

                // Update every 2 seconds
                await Task.Delay(2000, stoppingToken);
            }
        }

        private async Task SendVehicleUpdate(SimulatedVehicle vehicle, CancellationToken stoppingToken)
        {
            var vehicleData = new
            {
                vehicleId = $"V-{vehicle.Id:D4}",
                latitude = vehicle.Latitude,
                longitude = vehicle.Longitude,
                timestamp = DateTime.UtcNow
                // ... باقي الخصائص
            };
            
            // حدّث الـ cache
            VehicleHub.UpdateVehicleCache(vehicleData);
            
            // ارسل للـ clients المتصلين
            await _hubContext.Clients.All.SendAsync("ReceiveLocationUpdate", vehicleData, stoppingToken);
        }
    }
}
