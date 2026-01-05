using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FleetTracking.API.Hubs
{
    public class VehicleHub : Hub
    {
        public async Task SendLocationUpdate(int vehicleId, double lat, double lng)
        {
            await Clients.All.SendAsync("ReceiveLocationUpdate", vehicleId, lat, lng);
        }
    }
}
