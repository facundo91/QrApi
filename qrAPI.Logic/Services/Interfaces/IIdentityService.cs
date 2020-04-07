using System;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);

        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        Task<Person> GetPersonAsync(Guid userId);
    }
}
