using FleetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Requests
{
    public record GetVehiclesQuery
    {
        public VehicleStatus? Status { get; init; }
        public bool? IsOnline { get; init; }
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
