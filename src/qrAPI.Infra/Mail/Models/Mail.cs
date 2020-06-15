namespace qrAPI.Infra.Mail.Models
{
    public class Mail
    {
        public Mail(string from, string to, string subject, string html)
        {
            From = from;
            To = to;
            Subject = subject;
            Html = html;
        }
        public readonly string From;
        public readonly string To;
        public readonly string Subject;
        public readonly string Html;
    }
}