using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using qrAPI.Commands.Pets.ServiceCommands;
using qrAPI.Data;
using qrAPI.Domain;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Pets.ServiceHandlers.CommandHandlers
{
    public class CreatePetAsyncHandler : IRequestHandler<CreatePetAsyncCommand, Pet>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<PetDto> _petRepository;

        public CreatePetAsyncHandler(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _petRepository = dataContext.PetRepository;
        }

        public async Task<Pet> Handle(CreatePetAsyncCommand request, CancellationToken cancellationToken)
        {
            var petDto = _mapper.Map<PetDto>(request.PetToCreate);
            PetDto petCreated = await _petRepository.InsertAsync(petDto);
            return _mapper.Map<Pet>(petCreated);
        }
    }
}