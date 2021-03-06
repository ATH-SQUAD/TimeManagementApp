using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace TimeManagementApp.App.Auth
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, Message message);
    }
}
