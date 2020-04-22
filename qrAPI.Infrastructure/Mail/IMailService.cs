using System.Net.Http;
using System.Threading.Tasks;

namespace qrAPI.Infrastructure.Mail
{
    public interface IMailService
    {
        Task<HttpResponseMessage> SendSimpleMessage(string from, string to, string subject, string html);
    }
}
