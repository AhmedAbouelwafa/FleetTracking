using FleetTracking.Application.Features.Vehicle.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Validators
{
    public class VehicleLocationDtoValidator : AbstractValidator<VehicleLocationDto>
    {
        public VehicleLocationDtoValidator()
        {
            RuleFor(x => x.VehicleId)
                .GreaterThan(0)
                .WithMessage("Invalid vehicle ID");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180");

            RuleFor(x => x.Speed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Speed cannot be negative")
                .LessThanOrEqualTo(300)
                .WithMessage("Speed is unrealistic (greater than 300 km/h)");

            RuleFor(x => x.Heading)
                .InclusiveBetween(0, 360)
                .WithMessage("Heading must be between 0 and 360 degrees");

            RuleFor(x => x.Timestamp)
                .NotEmpty()
                .WithMessage("Timestamp is required")
                .LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(5))
                .WithMessage("Timestamp cannot be in the future")
                .GreaterThan(DateTime.UtcNow.AddDays(-1))
                .WithMessage("Timestamp is too old (more than 1 day)");
        }
    }

}
