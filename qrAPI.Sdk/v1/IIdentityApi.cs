using System.Threading.Tasks;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;
using Refit;

namespace qrAPI.Sdk.v1
{
    public interface IIdentityApi
    {
        [Post("/api/identity/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest registrationRequest);

        [Post("/api/identity/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest loginRequest);

        [Post("/api/identity/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshRequest);
    }
}