using AutoMapper;
using MediatR;
using qrAPI.Commands.Qrs.ServiceCommands;
using System.Threading;
using System.Threading.Tasks;
using qrAPI.Data;
using qrAPI.Domain;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Qrs.ServiceHandlers.CommandHandlers
{
    public class CreateQrAsyncHandler : IRequestHandler<CreateQrAsyncCommand, Qr>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<QrDto> _qrRepository;

        public CreateQrAsyncHandler(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _qrRepository = dataContext.QrRepository;
        }

        public async Task<Qr> Handle(CreateQrAsyncCommand request, CancellationToken cancellationToken)
        {
            var qrDto = _mapper.Map<QrDto>(request.QrToCreate);
            QrDto qrCreated = await _qrRepository.InsertAsync(qrDto);
            return _mapper.Map<Qr>(qrCreated);
        }
    }
}