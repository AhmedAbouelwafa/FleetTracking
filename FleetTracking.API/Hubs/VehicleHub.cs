using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace FleetTracking.API.Hubs
{
    public class VehicleHub : Hub
    {
        private static readonly ConcurrentDictionary<string, object> _vehicleCache = new();

        public async Task SendLocationUpdate(int vehicleId, double lat, double lng)
        {
            await Clients.All.SendAsync("ReceiveLocationUpdate", vehicleId, lat, lng);
        }

        public static void UpdateVehicleCache(object vehicleData)
        {
            try
            {
                var type = vehicleData.GetType();
                var prop = type.GetProperty("vehicleId");
                if (prop != null)
                {
                    var id = prop.GetValue(vehicleData)?.ToString();
                    if (!string.IsNullOrEmpty(id))
                    {
                        _vehicleCache.AddOrUpdate(id, vehicleData, (k, v) => vehicleData);
                    }
                }
            }
            catch
            {
                // Ignore errors
            }
        }
    }
}