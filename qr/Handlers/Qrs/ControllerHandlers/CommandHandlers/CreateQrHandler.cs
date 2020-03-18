using AutoMapper;
using MediatR;
using qrAPI.Commands.Qrs.ControllerCommands;
using qrAPI.Domain;
using qrAPI.Services;
using System.Threading;
using System.Threading.Tasks;

namespace qrAPI.Handlers.Qrs.ControllerHandlers.CommandHandlers
{
    public class CreateQrHandler : IRequestHandler<CreateQrCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IQrService _qrService;

        public CreateQrHandler(IMapper mapper, IQrService qrService)
        {
            _mapper = mapper;
            _qrService = qrService;
        }

        public async Task<bool> Handle(CreateQrCommand request, CancellationToken cancellationToken)
        {
            var qr = _mapper.Map<Qr>(request.CreateQrRequest);
            return await _qrService.CreateQrAsync(qr);
        }
    }
}