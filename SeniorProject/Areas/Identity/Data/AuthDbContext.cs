using AuthSystem.Areas.Identity.Data;
using MailChimp.Net.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using ServiceStack.Text;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Donation> Donations { get; set; }

        public DbSet<Events> Event { get; set; }

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
                    EventStart = "2023-06-02",
                    EventEnd = "2023-06-02"
            });
        }
    }
}