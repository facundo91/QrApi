using qrAPI.App.Domain;
using qrAPI.DAL.Dtos;
using System;
using System.Threading.Tasks;

namespace qrAPI.App.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);

        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        Task<User> GetPersonAsync(Guid userId);
    }
}
