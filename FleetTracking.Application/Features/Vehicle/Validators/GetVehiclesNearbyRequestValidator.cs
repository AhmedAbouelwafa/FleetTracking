using FleetTracking.Application.Features.Vehicle.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Validators
{
    public class GetVehiclesNearbyRequestValidator : AbstractValidator<GetVehiclesNearbyRequest>
    {
        public GetVehiclesNearbyRequestValidator()
        {
            RuleFor(x => x.Latitude)
                .NotEmpty()
                .WithMessage("Latitude is required")
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .NotEmpty()
                .WithMessage("Longitude is required")
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180");

            RuleFor(x => x.RadiusKm)
                .GreaterThan(0)
                .WithMessage("Radius must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Radius must not exceed 100 kilometers");
        }
    }

}
