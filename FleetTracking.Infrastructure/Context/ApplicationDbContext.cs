using FleetTracking.Core.Entites.Base;
using FleetTracking.Core.Entites.Models;
using FleetTracking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FleetTracking.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            DataSeeder.Seed(builder);

        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        private void UpdateTimestamps()
        {
            // base type بيجيب اكبر اب  اللى المفروض مش وارث
            var entries = ChangeTracker.Entries()
             .Where(e => e.Entity.GetType().BaseType?.IsGenericType == true &&
                e.Entity.GetType().BaseType.GetGenericTypeDefinition() == typeof(Entity<>));

            foreach (var entry in entries)
            {
                var entity = (dynamic)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedAt = DateTime.UtcNow;
                        entry.Property("CreatedAt").IsModified = false;
                        break;
                }
            }
        }

        public DbSet<Vehicle> Vehicles  { get; set;}

    }
}
