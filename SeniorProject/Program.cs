using EmailDatabase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EmailContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("EmailContext")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase= true;
        options.Password.RequireUppercase= true;
        options.Password.RequiredLength = 5;
    }
    )
    .AddEntityFrameworkStores<EmailContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.MapControllers();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
{
    await EmailContext.CreateTheAdmin(scope.ServiceProvider);
}

app.Run();
