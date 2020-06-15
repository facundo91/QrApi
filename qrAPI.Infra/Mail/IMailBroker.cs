using System.Net;
using System.Threading.Tasks;

namespace qrAPI.Infra.Mail
{
    public interface IMailBroker
    {
        Task<HttpStatusCode> SendSimpleMessage(Models.Mail mail);
    }
}
