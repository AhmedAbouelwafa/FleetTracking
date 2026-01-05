using FleetTracking.Core.Entites.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Infrastructure.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasIndex(v => v.PlateNumber).IsUnique();

            // 2️⃣ آخر تحديث (للـ Online/Offline)
            builder.HasIndex(v => v.LastUpdatedAt);

            // 3️⃣ الحالة (للفلترة)
            builder.HasIndex(v => v.Status);

            // 4️⃣ Soft Delete
            builder.HasIndex(v => v.IsDeleted);

        }
    }
}
