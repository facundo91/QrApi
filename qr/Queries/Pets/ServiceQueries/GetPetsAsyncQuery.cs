using System.Collections.Generic;
using MediatR;
using qrAPI.Domain;

namespace qrAPI.Queries.Pets.ServiceQueries
{
    public class GetPetsAsyncQuery : IRequest<IEnumerable<Pet>>
    {
    }
}
