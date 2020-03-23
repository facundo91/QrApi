using qrAPI.Domain;

namespace qrAPI.Services
{
    public interface IQrService<TDomainObject> : IGenericService<TDomainObject> 
        where TDomainObject : Qr
    {
    }
}