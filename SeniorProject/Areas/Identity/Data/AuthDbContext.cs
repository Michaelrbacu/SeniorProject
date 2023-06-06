using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using System;

namespace AuthSystem.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Donation> Donations { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Donation>().HasData(
                new Donation
                {
                    Id = 1,
                    Name = "test",
                    Email = "def@123.com",
                    Amount = 1,
                    Address = "112Test Dr",
                    Phone = "1234567890",
                    Message = "This is a test"
                });

            builder.Entity<Events>().HasData(
                new Events
                {
                    EventId = 1,
                    EventName = "test",
                    EventStart = new DateTime(2023, 6, 2),
                    EventEnd = new DateTime(2023, 6, 2)
                });
            builder.Entity<Admin>().HasData(
    new Admin
    {
        AdminId = 1,
        Email = "test@example.com",
    });
        }
    }
}