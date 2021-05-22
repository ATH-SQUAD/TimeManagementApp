//using System.Threading.Tasks;
//using Microsoft.Extensions.Options;
//using SendGrid;
//using SendGrid.Helpers.Mail;

//namespace TimeManagementApp.App.Auth
//{
//    public class EmailSender : IEmailSender
//    {
//        public EmailSender(IOptions<AuthMessageSenderOptions> optionAccessor)
//        {
//            Options = optionAccessor.Value;
//        }

//        public AuthMessageSenderOptions Options { get; set; }
//        public Task SendEmailAsync(string email, string subject, string message)
//        {
//            return Execute(Options.SendGridKey, subject, message, email);
//        }

//        public Task Execute(string apiKey, string subject, string message, string email)
//        {
//            var client = new SendGridClient(apiKey);
//            var msg = new SendGridMessage()
//            {
//                From = new EmailAddress("administrator@carbo.com.pl", Options.SendGridUser),
//                Subject = subject,
//                PlainTextContent = message,
//                HtmlContent = message
//            };
//            msg.AddTo(new EmailAddress(email));
//            msg.SetClickTracking(false, false);

//            return client.SendEmailAsync(msg);
//        }
//    }
//}
