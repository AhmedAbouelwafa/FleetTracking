using FleetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Responses
{
    public record VehicleDto
    {
        public int Id { get; init; }
        public string PlateNumber { get; init; }
        public string DriverName { get; init; }
        public double? LastLatitude { get; init; }
        public double? LastLongitude { get; init; }
        public double? LastSpeed { get; init; }
        public double? LastHeading { get; init; }
        public DateTime? LastUpdatedAt { get; init; }
        public VehicleStatus Status { get; init; }
        public bool IsActive { get; init; }
    }
}
