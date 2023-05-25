using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EmailDatabase.Models
{
    public class EmailContext : IdentityDbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options) : base(options) { }
        public DbSet<Email> Emails { get; set; }

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
        }

    }
}
