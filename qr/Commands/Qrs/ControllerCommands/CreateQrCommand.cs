using MediatR;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.Commands.Qrs.ControllerCommands
{
    public class CreateQrCommand : IRequest<QrResponse>
    {
        public CreateQrRequest CreateQrRequest { get; }

        public CreateQrCommand(CreateQrRequest createQrRequest)
        {
            CreateQrRequest = createQrRequest;
        }
    }
}