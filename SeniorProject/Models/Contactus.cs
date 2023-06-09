﻿using Microsoft.AspNetCore.Mvc;
using SeniorProject.Areas.Identity.EmailService;
using SeniorProject.Services;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly EmailSender _emailSender;

    public HomeController(EmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult ContactUs()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ContactUs(ContactUsConfirmationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var emailSubject = $"New Contact Us Message from {model.Name}";
            var emailMessage = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}";

            // Change this line to use SendEmailAsync and await the result
            bool  isSend=_emailSender.SendEmail("earthcareinitiative@outlook.com", emailSubject, emailMessage);

            return RedirectToAction("ContactUsConfirmation");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ContactUsConfirmation()
    {
        return View();
    }
}