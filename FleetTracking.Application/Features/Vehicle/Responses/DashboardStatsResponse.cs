using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Responses
{
    public record DashboardStatsResponse
    {
        public int TotalVehicles { get; init; }
        public int OnlineVehicles { get; init; }
        public int OfflineVehicles { get; init; }
        public int MovingVehicles { get; init; }
        public int StoppedVehicles { get; init; }
        public int IdleVehicles { get; init; }
    }
}
