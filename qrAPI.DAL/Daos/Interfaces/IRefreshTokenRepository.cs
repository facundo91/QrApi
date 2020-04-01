using System.Threading.Tasks;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken> GetByRefreshName(string refreshToken);
        public Task<bool> UpdateAsync(RefreshToken refreshToken);
        public Task<RefreshToken> InsertAsync(RefreshToken refreshToken);
    }
}
