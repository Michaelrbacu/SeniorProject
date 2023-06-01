﻿using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Donation> Donations { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Donation>().HasData(
                new Donation
                {
                    Id = 1,
                    Name = "test",
                    Email = "def@123.com",
                    Amount = 1,
                    Address = "112Test Dr",
                    Phone = 123-456-7890,
                    Message = "This is a test"
                
                });
        }
    }
}
