using AutoMapper;
using MediatR;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Queries.Qrs.ControllerQueries;
using qrAPI.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace qrAPI.Handlers.Qrs.ControllerHandlers.QueryHandlers
{
    public class GetAllQrsHandler : IRequestHandler<GetAllQrsQuery, IEnumerable<QrResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IQrService _qrService;

        public GetAllQrsHandler(IMapper mapper, IQrService qrService)
        {
            _mapper = mapper;
            _qrService = qrService;
        }

        public async Task<IEnumerable<QrResponse>> Handle(GetAllQrsQuery request, CancellationToken cancellationToken)
        {
            var qrs = await _qrService.GetQrsAsync();
            return _mapper.Map<IEnumerable<QrResponse>>(qrs);
        }
    }
}