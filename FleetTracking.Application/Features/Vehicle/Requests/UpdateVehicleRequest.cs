using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Requests
{
    public record UpdateVehicleRequest
    {
        [MaxLength(20)]
        public string? PlateNumber { get; init; }

        [MaxLength(100)]
        public string? DriverName { get; init; }

        public bool? IsActive { get; init; }
    }
}
