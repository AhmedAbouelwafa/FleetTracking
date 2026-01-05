using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Requests
{
    public record GetVehiclesNearbyRequest
    {
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; init; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; init; }

        [Range(0.1, 100)]
        public double RadiusKm { get; init; } = 5.0;
    }
}
