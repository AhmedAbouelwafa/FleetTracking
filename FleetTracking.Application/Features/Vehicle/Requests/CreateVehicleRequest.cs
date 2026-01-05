using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Application.Features.Vehicle.Requests
{
    public record CreateVehicleRequest
    {
        [Required(ErrorMessage = "رقم اللوحة مطلوب")]
        [MaxLength(20)]
        public string PlateNumber { get; init; }

        [Required(ErrorMessage = "اسم السواق مطلوب")]
        [MaxLength(100)]
        public string DriverName { get; init; }
    }
}
