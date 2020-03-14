using MediatR;
using qrAPI.Domain;
using System;

namespace qrAPI.Queries.Qrs.ServiceQueries
{
    public class GetQrByIdAsyncQuery : IRequest<Qr>
    {
        public Guid Id { get; }

        public GetQrByIdAsyncQuery(Guid id)
        {
            Id = id;
        }
    }
}