using Microsoft.EntityFrameworkCore;
using BloodGroupLocator.Web.Models;

namespace BloodGroupLocator.Web.Data
{
    public class BloodGroupLocatorContext : DbContext
    {
        public BloodGroupLocatorContext(DbContextOptions<BloodGroupLocatorContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Person entity
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
                entity.Property(e => e.BloodGroup).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Seed data
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Name = "John Doe",
                    Address = "123 Main St, New York, NY 10001",
                    BloodGroup = "O+",
                    Phone = "+1-555-0123",
                    Email = "john.doe@email.com",
                    Latitude = 40.7128,
                    Longitude = -74.0060,
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                },
                new Person
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Address = "456 Oak Ave, Los Angeles, CA 90210",
                    BloodGroup = "A+",
                    Phone = "+1-555-0456",
                    Email = "jane.smith@email.com",
                    Latitude = 34.0522,
                    Longitude = -118.2437,
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                },
                new Person
                {
                    Id = 3,
                    Name = "Mike Johnson",
                    Address = "789 Pine St, Chicago, IL 60601",
                    BloodGroup = "B+",
                    Phone = "+1-555-0789",
                    Email = "mike.johnson@email.com",
                    Latitude = 41.8781,
                    Longitude = -87.6298,
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}
