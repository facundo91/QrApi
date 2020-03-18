using MediatR;
using qrAPI.Commands.Qrs.ServiceCommands;
using System.Threading;
using System.Threading.Tasks;
using qrAPI.Data;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Qrs.ServiceHandlers.CommandHandlers
{
    public class DeleteQrAsyncHandler : IRequestHandler<DeleteQrAsyncCommand, bool>
    {
        private readonly IGenericRepository<QrDto> _qrRepository;

        public DeleteQrAsyncHandler(IDataContext dataContext)
        {
            _qrRepository = dataContext.QrRepository;
        }

        public async Task<bool> Handle(DeleteQrAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _qrRepository.DeleteAsync(request.QrId);
        }
    }
}