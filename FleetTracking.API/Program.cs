
using FleetTracking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using FleetTracking.Core.Interfaces.Repositories;
using FleetTracking.Infrastructure.Repositories;
using FleetTracking.Application.Contracts;
using FleetTracking.Application.Services;
using FleetTracking.API.Hubs;
using FleetTracking.API.Services;

namespace FleetTracking.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // SignalR
            builder.Services.AddSignalR();

            // Add services to the container.

            builder.Services.AddControllers();

            // 1. DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString).UseLazyLoadingProxies();
            });

            // Repositories
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

            // Services
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            
            // Notification Service
            builder.Services.AddScoped<IVehicleNotificationService, SignalRNotificationService>();

            // 5. Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "Target_Fleet_Tracking", Version = "v1" });

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Target Fleet Tracking API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<VehicleHub>("/vehicleHub");

            app.Run();
        }
    }
}
