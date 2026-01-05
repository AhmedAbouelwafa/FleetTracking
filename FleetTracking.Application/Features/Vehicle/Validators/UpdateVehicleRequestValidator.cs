using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Validators
{
    using FleetTracking.Application.Features.Vehicle.Requests;
    using FluentValidation;

    public class UpdateVehicleRequestValidator : AbstractValidator<UpdateVehicleRequest>
    {
        public UpdateVehicleRequestValidator()
        {
            When(x => x.PlateNumber != null, () =>
            {
                RuleFor(x => x.PlateNumber)
                    .MaximumLength(20)
                    .WithMessage("Plate number must not exceed 20 characters")
                    .Matches(@"^[\u0600-\u06FF\s\d]+$")
                    .WithMessage("Plate number must contain Arabic letters and numbers only");
            });

            When(x => x.DriverName != null, () =>
            {
                RuleFor(x => x.DriverName)
                    .MaximumLength(100)
                    .WithMessage("Driver name must not exceed 100 characters")
                    .MinimumLength(3)
                    .WithMessage("Driver name must be at least 3 characters");
            });
        }
    }
}
