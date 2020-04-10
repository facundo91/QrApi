using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using Refit;

namespace qrAPI.Sdk.v1
{
    public interface IPetsApi
    {
        [Get("/api/pets")]
        Task<ApiResponse<IEnumerable<PetResponse>>> GetAllAsync();

        [Get("/api/pets/{petId}")]
        Task<ApiResponse<PetResponse>> GetAsync(Guid petId);

        [Post("/api/pets")]
        Task<ApiResponse<PetResponse>> CreateAsync([Body] CreatePetRequest createPetRequest);

        [Put("/api/pets/{petId}")]
        Task<ApiResponse<HttpResponseMessage>> UpdateAsync(Guid petId, [Body] UpdatePetRequest updatePetRequest);

        [Delete("/api/pets/{petId}")]
        Task<ApiResponse<HttpResponseMessage>> DeleteAsync(Guid petId);
    }
}
