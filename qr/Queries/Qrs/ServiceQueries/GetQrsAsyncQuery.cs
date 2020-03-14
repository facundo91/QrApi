using MediatR;
using qrAPI.Domain;
using System.Collections.Generic;

namespace qrAPI.Queries.Qrs.ServiceQueries
{
    public class GetQrsAsyncQuery : IRequest<IEnumerable<Qr>>
    {
    }
}