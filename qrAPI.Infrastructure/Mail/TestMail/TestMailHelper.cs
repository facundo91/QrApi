using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace qrAPI.Infrastructure.Mail.TestMail
{
    public class TestMailHelper
    {
        private readonly TestMailSettings _testMailSettings;
        private readonly HttpClient _httpClient;

        public TestMailHelper(TestMailSettings testMailSettings, HttpClient httpClient)
        {
            _testMailSettings = testMailSettings;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_testMailSettings.BaseUrl);
        }

        public async Task<TestMailResponse> GetMails()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"?apikey={_testMailSettings.ApiKey}&namespace={_testMailSettings.Namespace}");
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TestMailResponse>(responseJson);
        }
    }
}
