using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
    public class SendGridEmailSender: IEmailSender
    {
        public SendGridEmailSender(
            IOptions<SendGridEmailSenderOptions> options
            ) 
        { 
            this.Options = options.Value;
        }
        public SendGridEmailSenderOptions Options { get; set; }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options.ApiKey, subject, message, email); 
        }
        private async Task<Response> Execute(
            string apiKey, 
            string subject, 
            string message, 
            string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.AddTo(new EmailAddress(Options.SenderEmail));
            msg.ReplyTo = new EmailAddress(email);
            msg.HtmlContent = message;
            return await client.SendEmailAsync(msg);

        }
    }
}
