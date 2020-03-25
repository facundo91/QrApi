using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;

namespace qrAPI.DAL.Data
{
    public interface IDataContext
    {
        void HealthCheck();
        IGenericRepository<T> GetRepository<T>() where T : Dto;
        IRefreshTokenRepository GetRefreshTokenRepository();
    }
}