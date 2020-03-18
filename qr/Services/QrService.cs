using MediatR;
using qrAPI.Commands.Qrs.ServiceCommands;
using qrAPI.Domain;
using qrAPI.Queries.Qrs.ServiceQueries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.Services
{
    public class QrService : IQrService
    {
        private readonly IMediator _mediator;

        public QrService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Qr>> GetQrsAsync()
        {
            var query = new GetQrsAsyncQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<Qr> GetQrByIdAsync(Guid qrId)
        {
            var query = new GetQrByIdAsyncQuery(qrId);
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<bool> CreateQrAsync(Qr qrToCreate)
        {
            var command = new CreateQrAsyncCommand(qrToCreate);
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<bool> UpdateQrAsync(Qr qrToUpdate)
        {
            var command = new UpdateQrAsyncCommand(qrToUpdate);
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<bool> DeleteQrAsync(Guid qrId)
        {
            var command = new DeleteQrAsyncCommand(qrId);
            var result = await _mediator.Send(command);
            return result;
        }
    }
}