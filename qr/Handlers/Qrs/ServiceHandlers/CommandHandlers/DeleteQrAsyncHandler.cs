using MediatR;
using qrAPI.Commands.Qrs.ServiceCommands;
using qrAPI.DAL.Data;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;
using System.Threading;
using System.Threading.Tasks;

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
            var result = await _qrRepository.DeleteAsync(request.QrId);
            return result;
        }
    }
}