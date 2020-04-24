using System.Net;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace qrAPI.Infrastructure.Mail.SendGrid
{
    public class SendGridMailService : IMailService
    {
        private readonly SendGridSettings _sendGridSettings;

        public SendGridMailService(SendGridSettings sendGridSettings)
        {
            _sendGridSettings = sendGridSettings;
        }
        public async Task<HttpStatusCode> SendSimpleMessage(string from, string to, string subject, string html)
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            var emailAddress = new EmailAddress(from, "QRight Thing");
            var plainTextContent = "Don't lose your dog again";
            var msg = MailHelper.CreateSingleEmail(emailAddress, new EmailAddress(to), subject, plainTextContent, html);
            return (await client.SendEmailAsync(msg)).StatusCode;
        }
    }
}
