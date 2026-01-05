using FleetTracking.Application.Contracts;
using FleetTracking.API.Hubs;
using FleetTracking.Core.Entites.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FleetTracking.API.Services
{
    public class SignalRNotificationService : IVehicleNotificationService
    {
        private readonly IHubContext<VehicleHub> _hubContext;

        public SignalRNotificationService(IHubContext<VehicleHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyVehicleUpdated(Vehicle vehicle)
        {
            // Broadcast the entire vehicle object or a specific update
            await _hubContext.Clients.All.SendAsync("ReceiveVehicleUpdate", vehicle);
        }
    }
}
