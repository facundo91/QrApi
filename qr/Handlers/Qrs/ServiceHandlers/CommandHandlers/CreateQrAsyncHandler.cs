using AutoMapper;
using MediatR;
using qrAPI.Commands.Qrs.ServiceCommands;
using System.Threading;
using System.Threading.Tasks;
using qrAPI.Data;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Qrs.ServiceHandlers.CommandHandlers
{
    public class CreateQrAsyncHandler : IRequestHandler<CreateQrAsyncCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<QrDto> _qrRepository;

        public CreateQrAsyncHandler(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _qrRepository = dataContext.QrRepository;
        }

        public async Task<bool> Handle(CreateQrAsyncCommand request, CancellationToken cancellationToken)
        {
            var qrDto = _mapper.Map<QrDto>(request.QrToCreate);
            return await _qrRepository.InsertAsync(qrDto);
        }
    }
}