using System.Threading.Tasks;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;
using Refit;

namespace qrAPI.Sdk.v1
{
    public interface IIdentityApi
    {
        [Post(ApiRoutes.Identity.Register)]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest registrationRequest);

        [Post(ApiRoutes.Identity.Login)]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest loginRequest);

        [Post(ApiRoutes.Identity.Refresh)]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshRequest);
    }
}