using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.Sdk.v1
{
    public interface IQrsApi
    {
        [Get("/api/qrs")]
        Task<ApiResponse<IEnumerable<QrResponse>>> GetAllAsync();

        [Get("/api/qrs/{qrId}")]
        Task<ApiResponse<QrResponse>> GetAsync(Guid qrId);

        [Post("/api/qrs")]
        Task<ApiResponse<QrResponse>> CreateAsync([Body] CreateQrRequest createQrRequest);

        [Put("/api/qrs/{qrId}")]
        Task<ApiResponse<bool>> UpdateAsync(Guid qrId, [Body] UpdateQrRequest updateQrRequest);

        [Delete("/api/qrs/{qrId}")]
        Task<ApiResponse<bool>> DeleteAsync(Guid qrId);
    }
}
