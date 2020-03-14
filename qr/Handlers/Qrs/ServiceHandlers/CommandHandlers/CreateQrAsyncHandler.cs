using AutoMapper;
using MediatR;
using qrAPI.Commands.Qrs.ServiceCommands;
using qrAPI.DAL.Data;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;
using qrAPI.Domain;
using System.Threading;
using System.Threading.Tasks;

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
            var qrCreated = await _qrRepository.InsertAsync(qrDto);
            var qr = _mapper.Map<Qr>(qrCreated);
            return qr;
        }
    }
}