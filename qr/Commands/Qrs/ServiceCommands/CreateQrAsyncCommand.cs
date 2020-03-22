using MediatR;
using qrAPI.Domain;

namespace qrAPI.Commands.Qrs.ServiceCommands
{
    public class CreateQrAsyncCommand : IRequest<Qr>
    {
        public Qr QrToCreate { get; }

        public CreateQrAsyncCommand(Qr qtToCreate)
        {
            QrToCreate = qtToCreate;
        }
    }
}