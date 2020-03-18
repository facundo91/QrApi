using MediatR;
using qrAPI.Contracts.v1.Requests;
using System;

namespace qrAPI.Commands.Qrs.ControllerCommands
{
    public class UpdateQrCommand : IRequest<bool>
    {
        public Guid QrId { get; }
        public UpdateQrRequest QrRequest { get; }

        public UpdateQrCommand(Guid qrId, UpdateQrRequest qrRequest)
        {
            QrId = qrId;
            QrRequest = qrRequest;
        }
    }
}