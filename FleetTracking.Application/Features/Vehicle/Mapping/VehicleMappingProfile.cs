using FleetTracking.Application.Features.Vehicle.Requests;
using FleetTracking.Application.Features.Vehicle.Responses;
using FleetTracking.Core.Enums;
using Mapster;

namespace FleetTracking.Application.Features.Vehicle.Mapping
{
    using Mapster;

    public class VehicleMappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // ============ Entity → DTO ============

            // Vehicle → VehicleDto
            config.NewConfig<Core.Entites.Models.Vehicle, VehicleDto>();

            // Vehicle → VehicleLocationDto
            config.NewConfig<Core.Entites.Models.Vehicle, VehicleLocationDto>()
                .Map(dest => dest.VehicleId, src => src.Id)
                .Map(dest => dest.Latitude, src => src.LastLatitude ?? 0)
                .Map(dest => dest.Longitude, src => src.LastLongitude ?? 0)
                .Map(dest => dest.Speed, src => src.LastSpeed ?? 0)
                .Map(dest => dest.Heading, src => src.LastHeading ?? 0)
                .Map(dest => dest.Timestamp, src => src.LastUpdatedAt ?? DateTime.UtcNow);

            // ============ Request → Entity ============

            // CreateVehicleRequest → Vehicle
            config.NewConfig<CreateVehicleRequest, Core.Entites.Models.Vehicle>()
                .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
                .Map(dest => dest.UpdatedAt, _ => DateTime.UtcNow)
                .Map(dest => dest.IsActive, _ => true)
                .Map(dest => dest.IsDeleted, _ => false)
                .Map(dest => dest.Status, _ => VehicleStatus.Offline)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.LastLatitude)
                .Ignore(dest => dest.LastLongitude)
                .Ignore(dest => dest.LastSpeed)
                .Ignore(dest => dest.LastHeading)
                .Ignore(dest => dest.LastUpdatedAt);

            // UpdateVehicleRequest → Vehicle
            config.NewConfig<UpdateVehicleRequest, Core.Entites.Models.Vehicle>()
                .Map(dest => dest.UpdatedAt, _ => DateTime.UtcNow)
                .IgnoreNullValues(true);

            // VehicleLocationDto → Vehicle (Update Location فقط)
            config.NewConfig<VehicleLocationDto, Core.Entites.Models.Vehicle>()
                .Map(dest => dest.LastLatitude, src => src.Latitude)
                .Map(dest => dest.LastLongitude, src => src.Longitude)
                .Map(dest => dest.LastSpeed, src => src.Speed)
                .Map(dest => dest.LastHeading, src => src.Heading)
                .Map(dest => dest.LastUpdatedAt, src => src.Timestamp)
                .Map(dest => dest.UpdatedAt, _ => DateTime.UtcNow)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.PlateNumber)
                .Ignore(dest => dest.DriverName)
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.IsActive)
                .Ignore(dest => dest.IsDeleted)
                .Ignore(dest => dest.Status);

            // ============ List Mappings ============

            // List<Vehicle> → List<VehicleDto>
            config.NewConfig<List<Core.Entites.Models.Vehicle>, List<VehicleDto>>();
        }
    }
}