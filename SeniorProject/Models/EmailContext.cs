using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SeniorProject.Models
{
    public class EmailContext : IdentityDbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options) : base(options) { }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public static async Task CreateTheAdmin(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string username = "admin";
            string password = "Aa11!";
            string roleName = "Admin";
            if(await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));

            }
            if(await userManager.FindByNameAsync(username) == null)
            {
                IdentityUser identityUser = new IdentityUser();
                identityUser.UserName = username;
                IdentityResult identityResult = await userManager.CreateAsync(identityUser, password);
                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(identityUser, roleName);
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Donation>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Email>().HasData(
                new Email
                {
                    EmailId = 1,
                    EmailAddress = "abc@123.com",
                    Password = "abc123"
                },
                new Email
                {
                    EmailId = 2,
                    EmailAddress = "def@123.com",
                    Password = "asdfasdfasdf"
                }
            );

            modelBuilder.Entity<Donation>().HasData(
                new Donation
                {
                    Id = 1,
                    Name = "test",
                    Email = "def@123.com",
                    Amount = 1
                }
            );
        }





    }
}
