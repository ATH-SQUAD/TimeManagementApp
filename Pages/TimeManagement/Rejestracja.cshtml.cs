using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;
using TimeManagementApp.App.Auth;
using System.Text.Encodings.Web;
using MimeKit;
using System.IO;
using MongoDB.Driver.Core.Events;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace TimeManagementApp.Pages.TimeManagement
{
    [AllowAnonymous]
    public class RejestracjaModel : PageModel
    {
        private readonly ILogger<RejestracjaModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public RejestracjaModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RejestracjaModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        [TempData]
        public string SuccessRegistrationMessage { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = RegisterViewModel.Email,
                    Email = RegisterViewModel.Email,
                    Firstname = RegisterViewModel.Firstname,
                    Lastname = RegisterViewModel.Lastname,
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture)
                };

                IdentityResult createUser = await _userManager.CreateAsync(user, RegisterViewModel.Password);
                if (createUser.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Get TemplateFile located at wwwroot/Template/EmailTemplates/RegisterConfirmation.html.
                    string pathToFile = _env.WebRootPath
                           + Path.DirectorySeparatorChar.ToString()
                           + "Template"
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailTemplates"
                           + Path.DirectorySeparatorChar.ToString()
                           + "RegisterConfirmation.html";

                    BodyBuilder builder = new BodyBuilder();
                    using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                    {
                        builder.HtmlBody = SourceReader.ReadToEnd();
                    }

                    string subject = "Rejestracja użytkownika";
                    string date = string.Format("{0:dddd, d MMMM yyyy}", DateTime.Now);
                    string info = "Aby zmienić hasło kliknij na poniższy przycisk";
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string callbackUrl = Url.Page(
                        "/TimeManagement/EmailConfirmation",
                        pageHandler: null,
                        values: new { userId = user.Id, code },
                        protocol: Request.Scheme);
                    string messageBody = string.Format(builder.HtmlBody,
                                            subject,
                                            date,
                                            RegisterViewModel.Email,
                                            RegisterViewModel.Email,
                                            RegisterViewModel.Password,
                                            info,
                                            callbackUrl
                                            );
                    Message message = new Message(new string[] { RegisterViewModel.Email }, "Confirm yours account", messageBody, null);
                    await _emailSender.SendEmailAsync(RegisterViewModel.Email, message);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    SuccessRegistrationMessage = "Konto zostało pomyślnie utworzone!";
                    return RedirectToPage("./Logowanie");
                }

                ModelState.AddModelError(string.Empty, "Podany Email jest już w użyciu");
            }

            return Page();
        }
    }
}