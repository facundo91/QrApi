namespace qrAPI.Infra.Mail.SendGun
{
    public class SendGunSettings
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string RequestUri { get; set; }
        public string From { get; set; }
        public bool Enabled { get; set; }
    }
}
