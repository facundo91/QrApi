using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using qrAPI.Commands.Pets.ControllerCommands;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Domain;
using qrAPI.Services;

namespace qrAPI.Handlers.Pets.ControllerHandlers.CommandHandlers
{
    public class CreatePetHandler : IRequestHandler<CreatePetCommand, PetResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPetService _petService;

        public CreatePetHandler(IMapper mapper, IPetService petService)
        {
            _mapper = mapper;
            _petService = petService;
        }

        public async Task<PetResponse> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = _mapper.Map<Pet>(request.CreatePetRequest);
            Pet petCreated = await _petService.CreatePetAsync(pet);
            return _mapper.Map<PetResponse>(petCreated);
        }
    }
}