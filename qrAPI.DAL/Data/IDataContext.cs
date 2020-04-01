using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data
{
    public interface IDataContext
    {
        void HealthCheck();
        IRepository<T> GetRepository<T>() where T : Dto;
        IRefreshTokenRepository GetRefreshTokenRepository();
    }
}