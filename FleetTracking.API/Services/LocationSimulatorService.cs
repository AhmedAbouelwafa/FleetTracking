using FleetTracking.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FleetTracking.API.Services
{
    // ✅ الـ class مع كل الحقول المطلوبة
    public class SimulatedVehicle
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Heading { get; set; }
        public double Speed { get; set; }
        public string Status { get; set; } = "moving";
        public string PlateNumber { get; set; } = "";
        public string DriverName { get; set; } = "";
    }

    public class LocationSimulatorService : BackgroundService
    {
        private readonly IHubContext<VehicleHub> _hubContext;
        private readonly List<SimulatedVehicle> _vehicles;
        private readonly Random _random = new Random();

        public LocationSimulatorService(IHubContext<VehicleHub> hubContext)
        {
            _hubContext = hubContext;

            // ✅ Initialize مع كل البيانات
            _vehicles = new List<SimulatedVehicle>();
            for (int i = 1; i <= 5; i++)
            {
                _vehicles.Add(new SimulatedVehicle
                {
                    Id = i,
                    Latitude = 24.7136 + (i * 0.01),
                    Longitude = 46.6753 + (i * 0.01),
                    Heading = _random.NextDouble() * 360,
                    Speed = 30 + _random.NextDouble() * 50,
                    Status = "moving",
                    PlateNumber = $"ABC {1000 + i}",
                    DriverName = $"Driver {i}"
                });
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var vehicle in _vehicles)
                {
                    // ✅ Simulate movement
                    vehicle.Latitude += (_random.NextDouble() - 0.5) * 0.001;
                    vehicle.Longitude += (_random.NextDouble() - 0.5) * 0.001;
                    vehicle.Heading = (vehicle.Heading + (_random.NextDouble() - 0.5) * 30) % 360;
                    if (vehicle.Heading < 0) vehicle.Heading += 360;
                    vehicle.Speed = 30 + _random.NextDouble() * 50;

                    // ✅ Occasionally change status
                    if (_random.NextDouble() < 0.05)
                    {
                        vehicle.Status = vehicle.Status == "moving" ? "idle" : "moving";
                        vehicle.Speed = vehicle.Status == "idle" ? 0 : 30 + _random.NextDouble() * 50;
                    }

                    await SendVehicleUpdate(vehicle, stoppingToken);
                }

                await Task.Delay(2000, stoppingToken);
            }
        }

        // ✅ ارسل كل الحقول
        private async Task SendVehicleUpdate(SimulatedVehicle vehicle, CancellationToken stoppingToken)
        {
            var vehicleData = new
            {
                vehicleId = $"V-{vehicle.Id:D4}",
                lat = vehicle.Latitude,
                lng = vehicle.Longitude,
                latitude = vehicle.Latitude,
                longitude = vehicle.Longitude,
                heading = vehicle.Heading,
                speed = vehicle.Speed,
                status = vehicle.Status,
                plateNumber = vehicle.PlateNumber,
                driverName = vehicle.DriverName,
                timestamp = DateTime.UtcNow.ToString("o")
            };

            // ✅ حفظ في الـ cache
            VehicleHub.UpdateVehicleCache(vehicleData);

            // ✅ ارسل للـ clients
            await _hubContext.Clients.All.SendAsync("ReceiveLocationUpdate", vehicleData, stoppingToken);
        }
    }
}