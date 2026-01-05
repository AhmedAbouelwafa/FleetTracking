using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Validators
{
    using FleetTracking.Application.Features.Vehicle.Requests;
    using FluentValidation;

    public class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
    {
        public CreateVehicleRequestValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty()
                .WithMessage("Plate number is required")
                .MaximumLength(20)
                .WithMessage("Plate number must not exceed 20 characters")
                .Matches(@"^[\u0600-\u06FF\s\d]+$")
                .WithMessage("Plate number must contain Arabic letters and numbers only");

            RuleFor(x => x.DriverName)
                .NotEmpty()
                .WithMessage("Driver name is required")
                .MaximumLength(100)
                .WithMessage("Driver name must not exceed 100 characters")
                .MinimumLength(3)
                .WithMessage("Driver name must be at least 3 characters");
        }
    }

}
