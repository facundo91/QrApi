using System.Net;
using System.Threading.Tasks;

namespace qrAPI.Infrastructure.Mail
{
    public interface IMailService
    {
        Task<HttpStatusCode> SendSimpleMessage(string @from, string to, string subject, string html);
    }
}
