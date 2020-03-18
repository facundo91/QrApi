using MediatR;
using qrAPI.Domain;

namespace qrAPI.Commands.Qrs.ServiceCommands
{
    public class UpdateQrAsyncCommand : IRequest<bool>
    {
        public Qr QrToUpdate { get; }

        public UpdateQrAsyncCommand(Qr qtToUpdate)
        {
            QrToUpdate = qtToUpdate;
        }
    }
}