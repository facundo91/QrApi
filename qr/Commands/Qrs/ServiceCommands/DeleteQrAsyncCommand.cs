using MediatR;
using System;

namespace qrAPI.Commands.Qrs.ServiceCommands
{
    public class DeleteQrAsyncCommand : IRequest<bool>
    {
        public Guid QrId { get; }

        public DeleteQrAsyncCommand(Guid qrId)
        {
            QrId = qrId;
        }
    }
}