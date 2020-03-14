using MediatR;
using qrAPI.Contracts.v1.Responses;
using System.Collections.Generic;

namespace qrAPI.Queries.Qrs.ControllerQueries
{
    public class GetAllQrsQuery : IRequest<IEnumerable<QrResponse>>
    {
    }
}