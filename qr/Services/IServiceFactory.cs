using qrAPI.Domain;

namespace qrAPI.Services
{
    public interface IServiceFactory
    {
        IGenericService<T> GetService<T>() where T : DomainObject;
    }
}