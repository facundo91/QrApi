using qrAPI.Domain;

namespace qrAPI.Services
{
    public interface IPetService<TDomainObject> : IGenericService<TDomainObject> 
        where TDomainObject : Pet
    {
    }
}
