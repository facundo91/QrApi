using AutoMapper;
using MediatR;
using qrAPI.Commands.Qrs.ControllerCommands;
using qrAPI.Domain;
using qrAPI.Services;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.FeatureManagement;
using qrAPI.Options;

namespace qrAPI.Handlers.Qrs.ControllerHandlers.CommandHandlers
{
    public class UpdateQrHandler : IRequestHandler<UpdateQrCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IQrService _qrService;
        private readonly IFeatureManager _featureManager;

        public UpdateQrHandler(IMapper mapper, IQrService qrService, IFeatureManager featureManager)
        {
            _mapper = mapper;
            _qrService = qrService;
            _featureManager = featureManager;
        }

        public async Task<bool> Handle(UpdateQrCommand request, CancellationToken cancellationToken)
        {
            if (!_featureManager.IsEnabledAsync(nameof(FeatureFlags.AbTestFlag)).Result) return false;
            var qr = _mapper.Map<Qr>(request.QrRequest);
            return await _qrService.UpdateQrAsync(qr);
        }
    }
}