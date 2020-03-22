using System.Collections.Generic;
using MediatR;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.Queries.Pets.ControllerQueries
{
    public class GetAllPetsQuery : IRequest<IEnumerable<PetResponse>>
    {
    }
}
