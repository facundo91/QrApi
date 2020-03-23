using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public interface IPetService<TDomainObject> : IGenericService<TDomainObject> 
        where TDomainObject : Pet
    {
    }
}
