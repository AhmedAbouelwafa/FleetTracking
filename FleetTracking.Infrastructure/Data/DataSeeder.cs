using FleetTracking.Core.Entites.Models;
using FleetTracking.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            builder.Entity<Vehicle>().HasData(
                // 1️⃣ سيارة متحركة
                new Vehicle
                {
                    Id = 1,
                    PlateNumber = "أ ب ج 1234",
                    DriverName = "أحمد محمد",
                    LastLatitude = 30.0444,
                    LastLongitude = 31.2357,
                    LastSpeed = 65.5,
                    LastHeading = 90.0,
                    LastUpdatedAt = seedDate.AddMinutes(-2),
                    Status = VehicleStatus.Moving,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-30),
                    UpdatedAt = seedDate.AddMinutes(-2)
                },

                // 2️⃣ سيارة واقفة
                new Vehicle
                {
                    Id = 2,
                    PlateNumber = "د هـ و 5678",
                    DriverName = "محمود علي",
                    LastLatitude = 30.0626,
                    LastLongitude = 31.3547,
                    LastSpeed = 0.0,
                    LastHeading = 45.0,
                    LastUpdatedAt = seedDate.AddMinutes(-3),
                    Status = VehicleStatus.Stopped,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-25),
                    UpdatedAt = seedDate.AddMinutes(-3)
                },

                // 3️⃣ سيارة Idle
                new Vehicle
                {
                    Id = 3,
                    PlateNumber = "ز ح ط 9012",
                    DriverName = "خالد حسن",
                    LastLatitude = 31.2001,
                    LastLongitude = 29.9187,
                    LastSpeed = 0.0,
                    LastHeading = 180.0,
                    LastUpdatedAt = seedDate.AddMinutes(-8),
                    Status = VehicleStatus.Idle,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-20),
                    UpdatedAt = seedDate.AddMinutes(-8)
                },

                // 4️⃣ سيارة Offline
                new Vehicle
                {
                    Id = 4,
                    PlateNumber = "ي ك ل 3456",
                    DriverName = "عمر سعيد",
                    LastLatitude = 26.8206,
                    LastLongitude = 30.8025,
                    LastSpeed = 0.0,
                    LastHeading = 270.0,
                    LastUpdatedAt = seedDate.AddMinutes(-15),
                    Status = VehicleStatus.Offline,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-15),
                    UpdatedAt = seedDate.AddMinutes(-15)
                },

                // 5️⃣ سيارة متحركة بسرعة عالية
                new Vehicle
                {
                    Id = 5,
                    PlateNumber = "م ن س 7890",
                    DriverName = "يوسف إبراهيم",
                    LastLatitude = 30.5852,
                    LastLongitude = 30.9922,
                    LastSpeed = 110.0,
                    LastHeading = 315.0,
                    LastUpdatedAt = seedDate.AddMinutes(-1),
                    Status = VehicleStatus.Moving,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-10),
                    UpdatedAt = seedDate.AddMinutes(-1)
                },

                // 6️⃣ سيارة جديدة (مفيش موقع بعد)
                new Vehicle
                {
                    Id = 6,
                    PlateNumber = "ع ف ص 2468",
                    DriverName = "حسن عبدالله",
                    LastLatitude = null,
                    LastLongitude = null,
                    LastSpeed = null,
                    LastHeading = null,
                    LastUpdatedAt = null,
                    Status = VehicleStatus.Offline,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddHours(-2),
                    UpdatedAt = seedDate.AddHours(-2)
                },

                // 7️⃣ سيارة في الصعيد
                new Vehicle
                {
                    Id = 7,
                    PlateNumber = "ق ر ش 1357",
                    DriverName = "مصطفى أحمد",
                    LastLatitude = 25.6872,
                    LastLongitude = 32.6396,
                    LastSpeed = 45.0,
                    LastHeading = 0.0,
                    LastUpdatedAt = seedDate.AddMinutes(-5),
                    Status = VehicleStatus.Moving,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-5),
                    UpdatedAt = seedDate.AddMinutes(-5)
                },

                // 8️⃣ سيارة غير نشطة
                new Vehicle
                {
                    Id = 8,
                    PlateNumber = "ت ث خ 9753",
                    DriverName = "سامي وليد",
                    LastLatitude = 30.0444,
                    LastLongitude = 31.2357,
                    LastSpeed = 0.0,
                    LastHeading = 0.0,
                    LastUpdatedAt = seedDate.AddDays(-7),
                    Status = VehicleStatus.Offline,
                    IsActive = false,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-60),
                    UpdatedAt = seedDate.AddDays(-7)
                },

                // 9️⃣ سيارة في الساحل
                new Vehicle
                {
                    Id = 9,
                    PlateNumber = "ذ ض ظ 8642",
                    DriverName = "طارق فتحي",
                    LastLatitude = 31.0409,
                    LastLongitude = 28.9617,
                    LastSpeed = 75.0,
                    LastHeading = 270.0,
                    LastUpdatedAt = seedDate.AddSeconds(-30),
                    Status = VehicleStatus.Moving,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-3),
                    UpdatedAt = seedDate.AddSeconds(-30)
                },

                // 🔟 سيارة في القناة
                new Vehicle
                {
                    Id = 10,
                    PlateNumber = "غ إ أ 7531",
                    DriverName = "وليد صلاح",
                    LastLatitude = 30.5833,
                    LastLongitude = 32.2667,
                    LastSpeed = 55.0,
                    LastHeading = 135.0,
                    LastUpdatedAt = seedDate.AddMinutes(-4),
                    Status = VehicleStatus.Moving,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = seedDate.AddDays(-12),
                    UpdatedAt = seedDate.AddMinutes(-4)
                }
            );
        }
    }
}
