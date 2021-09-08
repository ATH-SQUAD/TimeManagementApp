using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeManagementApp.App.Auth;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;
using System.Threading.Tasks;
using MimeKit;
using System.Text.Encodings.Web;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver.Core.Events;
using EllipticCurve;
using System.Security.Claims;

namespace TimeManagementApp.Pages.TimeManagement
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _env;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager,
                                   IEmailSender emailSender,
                                   IWebHostEnvironment env)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _env = env;
        }

        [BindProperty]
        public ForgotPasswordViewModel ResetLink { get; set; }
        [TempData]
        public string SuccessSendMessage { get; set; }

        public async Task<IActionResult> OnPostAsync(string userName, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                userName = User.FindFirstValue(ClaimTypes.Name); // Will give the user's userName.
                user = await _userManager.FindByEmailAsync(ResetLink.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed.
                    return Page();
                }

                // Get TemplateFile located at wwwroot/Templates/EmailTemplate/Confirmation.html.
                var pathToFile = _env.WebRootPath
                        + Path.DirectorySeparatorChar.ToString()
                        + "Template"
                        + Path.DirectorySeparatorChar.ToString()
                        + "EmailTemplates"
                        + Path.DirectorySeparatorChar.ToString()
                        + "Confirmation.html";


                BodyBuilder builder = new BodyBuilder();

                string pass = "hasło";
                string subject = "";
                string info = "Aby zmienić hasło kliknij na poniższy przycisk";

                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                string callbackUrl = Url.Page(
                    "/TimeManagement/ZmianaHasla",
                    pageHandler: null,
                    values: new { userId = user.Email, token },
                    protocol: Request.Scheme);

                string Date = string.Format("{0:dddd, d MMMM yyyy}", DateTime.Now);
                string messageBody = string.Format(builder.HtmlBody,
                                        subject,
                                        Date,
                                        ResetLink.Email,
                                        userName,
                                        pass,
                                        info,
                                        callbackUrl
                                        );
                //{0} : Subject
                //{1} : Current DateTime
                //{2} : Email
                //{3} : Username
                //{4} : Password
                //{5} : Message
                //{6} : callbackURL
                Message message = new Message(new string[] { ResetLink.Email }, "Reset password token", messageBody, null);
                await _emailSender.SendEmailAsync(ResetLink.Email, message);
                ViewData["MessageValue"] = "0";
                TempData["SuccessSendMessage"] = "Link do zmiany hasła został wysłany na twojego maila!";

                return RedirectToPage("./Logowanie");
            }

            return Page();
        }
    }
}