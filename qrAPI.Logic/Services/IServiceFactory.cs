using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public interface IServiceFactory
    {
        IGenericService<T> GetService<T>() where T : DomainObject;
    }
}