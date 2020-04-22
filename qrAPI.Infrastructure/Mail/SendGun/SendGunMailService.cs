using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace qrAPI.Infrastructure.Mail.SendGun
{
    public class SendGunMailService : IMailService
    {
        private readonly SendGunSettings _sendGunSettings;
        private readonly HttpClient _httpClient;

        public SendGunMailService(SendGunSettings sendGunSettings, HttpClient httpClient)
        {
            _sendGunSettings = sendGunSettings;
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendSimpleMessage(string from, string to, string subject, string html)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", _sendGunSettings.From),
                new KeyValuePair<string, string>("to", to),
                new KeyValuePair<string, string>("subject", subject),
                new KeyValuePair<string, string>("html", html)
            });

            return await _httpClient.PostAsync(_sendGunSettings.RequestUri,
                 content);
        }
    }
}
