using qrAPI.Logic.Domain;
using qrAPI.Logic.Services;

namespace qrAPI.Logic.Factory
{
    public interface IServiceFactory
    {
        IGenericService<T> GetService<T>() where T : DomainObject;
        IMedicalRecordService GetMedicalRecordService();
    }
}