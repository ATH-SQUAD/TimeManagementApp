using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace TimeManagementApp.App.Auth
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.UserName, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        public async Task SendEmailAsync(string email, Message message)
        {
            MimeMessage mailMessage = CreateEmailMessage(message);

            await SendEmailAsync(mailMessage);
        }

        private async Task SendEmailAsync(MimeMessage mailMessage)
        {
            using SmtpClient client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                await client.SendAsync(mailMessage);
            }
            catch
            {
                // TO-DO: Log an error message or throw an exception, or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                /*client.Dispose();*/
            }
        }
    }

    //public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
    //{
    //    Options = optionsAccessor.Value;
    //}

    //public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

    //public Task SendEmailAsync(string email, string subject, string message)
    //{
    //    return Execute(Options.SendGridKey, subject, message, email);
    //}

    //public Task Execute(string apiKey, string subject, string message, string email)
    //{
    //    var client = new SendGridClient(apiKey);
    //    var msg = new SendGridMessage()
    //    {
    //        From = new EmailAddress("Joe@contoso.com", Options.SendGridUser),
    //        Subject = subject,
    //        PlainTextContent = message,
    //        HtmlContent = message
    //    };
    //    msg.AddTo(new EmailAddress(email));

    //    // Disable click tracking.
    //    // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
    //    msg.SetClickTracking(false, false);

    //    return client.SendEmailAsync(msg);
    //}
}

