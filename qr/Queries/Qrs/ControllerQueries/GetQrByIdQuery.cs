using MediatR;
using qrAPI.Contracts.v1.Responses;
using System;

namespace qrAPI.Queries.Qrs.ControllerQueries
{
    public class GetQrByIdQuery : IRequest<QrResponse>
    {
        public Guid Id { get; }

        public GetQrByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}