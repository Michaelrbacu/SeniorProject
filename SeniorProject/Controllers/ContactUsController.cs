using Microsoft.AspNetCore.Mvc;
using SeniorProject.Areas.Identity.EmailService;
using SeniorProject.Services;
using System.Threading.Tasks;

public class ContactUsController : Controller
{
    private readonly EmailSender _emailSender;

    public ContactUsController(EmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ContactUs(ContactUsViewModel model)
    {
        if (ModelState.IsValid)
        {
            var emailSubject = $"New Contact Us Message from {model.Name}";
            var emailMessage = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}";

            // Change this line to use SendEmailAsync and await the result
            await _emailSender.SendEmailAsync("earthcareinitiative@outlook.com", emailSubject, emailMessage);

            var confirmationModel = new ContactUsConfirmationViewModel
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message
            };

            return RedirectToAction("Confirmation");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Confirmation()
    {
        return View();
    }
}