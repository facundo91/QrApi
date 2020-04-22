using System.Collections.Generic;

namespace qrAPI.Infrastructure.Mail.TestMail
{
    public class TestMailResponse
    {
        public string result { get; set; }
        public object message { get; set; }
        public int count { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<Email> emails { get; set; }
    }
}
