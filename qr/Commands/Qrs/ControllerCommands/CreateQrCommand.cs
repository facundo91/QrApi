using MediatR;
using qrAPI.Contracts.v1.Requests;

namespace qrAPI.Commands.Qrs.ControllerCommands
{
    public class CreateQrCommand : IRequest<bool>
    {
        public CreateQrRequest CreateQrRequest { get; }

        public CreateQrCommand(CreateQrRequest createQrRequest)
        {
            CreateQrRequest = createQrRequest;
        }
    }
}