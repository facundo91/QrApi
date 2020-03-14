using MediatR;
using qrAPI.Commands.Qrs.ControllerCommands;
using qrAPI.Services;
using System.Threading;
using System.Threading.Tasks;

namespace qrAPI.Handlers.Qrs.ControllerHandlers.CommandHandlers
{
    public class DeleteQrHandler : IRequestHandler<DeleteQrCommand, bool>
    {
        private readonly IQrService _qrService;

        public DeleteQrHandler(IQrService qrService)
        {
            _qrService = qrService;
        }

        public async Task<bool> Handle(DeleteQrCommand request, CancellationToken cancellationToken)
        {
            return await _qrService.DeleteQrAsync(request.QrId);
        }
    }
}