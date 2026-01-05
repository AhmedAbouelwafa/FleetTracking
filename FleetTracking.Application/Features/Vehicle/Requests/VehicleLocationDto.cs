using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Requests
{
    public record VehicleLocationDto
    {
        public int VehicleId { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public double Speed { get; init; }
        public double Heading { get; init; }
        public DateTime Timestamp { get; init; }
    }
}
