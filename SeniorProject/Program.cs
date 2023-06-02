using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Google;
using SeniorProject.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using SeniorProject.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>();

// Add Google authentication service
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

// Register the EmailSettings and EmailSender services
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Added this line
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

// Define the EmailSender class outside the WebApplication builder
public class EmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string messages)
    {
        try
        {

        MailAddress from = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
        MailAddress to = new MailAddress(email);
        MailMessage message = new MailMessage(from, to);
        message.Body = messages;
        message.Subject = subject;
        message.IsBodyHtml = false;
        string server = _emailSettings.Server;
        SmtpClient client = new SmtpClient(server);
        client.UseDefaultCredentials = Convert.ToBoolean("false");
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Port = Convert.ToInt16(_emailSettings.Port);
        client.EnableSsl = Convert.ToBoolean(_emailSettings.UseSSL);
        client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
        client.Timeout = Convert.ToInt16(9000);
         await client.SendMailAsync(message);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}