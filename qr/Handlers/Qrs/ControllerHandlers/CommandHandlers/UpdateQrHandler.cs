using AutoMapper;
using MediatR;
using qrAPI.Commands.Qrs.ControllerCommands;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Domain;
using qrAPI.Services;
using System.Threading;
using System.Threading.Tasks;

namespace qrAPI.Handlers.Qrs.ControllerHandlers.CommandHandlers
{
    public class UpdateQrHandler : IRequestHandler<UpdateQrCommand, QrResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQrService _qrService;

        public UpdateQrHandler(IMapper mapper, IQrService qrService)
        {
            _mapper = mapper;
            _qrService = qrService;
        }

        public async Task<QrResponse> Handle(UpdateQrCommand request, CancellationToken cancellationToken)
        {
            var qr = _mapper.Map<Qr>(request.QrRequest);
            var qrUpdated = await _qrService.UpdateQrAsync(qr);
            return qrUpdated == null ? null : _mapper.Map<QrResponse>(qr);
        }
    }
}