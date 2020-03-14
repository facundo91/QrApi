using MediatR;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;
using System;

namespace qrAPI.Commands.Qrs.ControllerCommands
{
    public class UpdateQrCommand : IRequest<QrResponse>
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