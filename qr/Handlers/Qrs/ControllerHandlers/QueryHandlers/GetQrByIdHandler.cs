using AutoMapper;
using MediatR;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Queries.Qrs.ControllerQueries;
using qrAPI.Services;
using System.Threading;
using System.Threading.Tasks;

namespace qrAPI.Handlers.Qrs.ControllerHandlers.QueryHandlers
{
    public class GetQrByIdHandler : IRequestHandler<GetQrByIdQuery, QrResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQrService _qrService;

        public GetQrByIdHandler(IMapper mapper, IQrService qrService)
        {
            _mapper = mapper;
            _qrService = qrService;
        }

        public async Task<QrResponse> Handle(GetQrByIdQuery request, CancellationToken cancellationToken)
        {
            var qr = await _qrService.GetQrByIdAsync(request.Id);
            return qr != null ? _mapper.Map<QrResponse>(qr) : null;
        }
    }
}