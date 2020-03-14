using MediatR;
using System;

namespace qrAPI.Commands.Qrs.ControllerCommands
{
    public class DeleteQrCommand : IRequest<bool>
    {
        public Guid QrId { get; }

        public DeleteQrCommand(Guid qrId)
        {
            QrId = qrId;
        }
    }
}