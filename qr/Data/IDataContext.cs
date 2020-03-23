using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Data
{
    public interface IDataContext
    {
        void HealthCheck();
        IGenericRepository<T> GetRepository<T>() where T : Dto;
    }
}