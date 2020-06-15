using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace qrAPI.Infra.Mail.SendGrid
{
    public class SendGridMailBroker : IMailBroker
    {
        private readonly SendGridSettings _sendGridSettings;

        public SendGridMailBroker(SendGridSettings sendGridSettings)
        {
            _sendGridSettings = sendGridSettings;
        }
        public async Task<HttpStatusCode> SendSimpleMessage(Models.Mail mail)
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            var emailAddress = new EmailAddress(mail.From, "QRight Thing");
            var plainTextContent = "Don't lose your dog again";
            var msg = MailHelper.CreateSingleEmail(emailAddress, new EmailAddress(mail.To), mail.Subject, plainTextContent, mail.Html);
            return (await client.SendEmailAsync(msg)).StatusCode;
        }
    }
}
