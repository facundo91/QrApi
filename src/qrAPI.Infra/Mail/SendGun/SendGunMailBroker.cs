using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace qrAPI.Infra.Mail.SendGun
{
    public class SendGunMailBroker : IMailBroker
    {
        private readonly SendGunSettings _sendGunSettings;
        private readonly HttpClient _httpClient;

        public SendGunMailBroker(SendGunSettings sendGunSettings, HttpClient httpClient)
        {
            _sendGunSettings = sendGunSettings;
            _httpClient = httpClient;
        }

        public async Task<HttpStatusCode> SendSimpleMessage(Models.Mail mail)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", _sendGunSettings.From),
                new KeyValuePair<string, string>("to", mail.To),
                new KeyValuePair<string, string>("subject", mail.Subject),
                new KeyValuePair<string, string>("html", mail.Html)
            });
            return (await _httpClient.PostAsync(_sendGunSettings.RequestUri, content)).StatusCode;
        }
    }
}
