using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public interface IQrService<TDomainObject> : IGenericService<TDomainObject> 
        where TDomainObject : Qr
    {
    }
}