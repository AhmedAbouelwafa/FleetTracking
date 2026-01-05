using FleetTracking.Core.Entites.Models;
using System.Threading.Tasks;

namespace FleetTracking.Application.Contracts
{
    public interface IVehicleNotificationService
    {
        Task NotifyVehicleUpdated(Vehicle vehicle);
    }
}
