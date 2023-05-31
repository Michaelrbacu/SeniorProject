using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Facebook;
using SeniorProject.Areas.Admin.Controllers; // Updated namespace import
using SeniorProject.Services; // Import the namespace for EmailSender and EmailSettings

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthDbContext>();

// Add Facebook authentication
builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = configuration["Facebook:AppId"];
        options.AppSecret = configuration["Facebook:AppSecret"];
        options.Scope.Add("email");
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

// Register the EmailSender and configure EmailSettings
builder.Services.AddTransient<EmailSender>(); // Register the EmailSender
builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings")); // Configure EmailSettings from appsettings.json

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add this line to enable authentication middleware
app.UseAuthorization();

app.MapControllers(); // Add this line to enable MVC controllers

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();