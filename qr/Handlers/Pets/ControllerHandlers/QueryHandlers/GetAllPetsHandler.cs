using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Queries.Pets.ControllerQueries;
using qrAPI.Services;

namespace qrAPI.Handlers.Pets.ControllerHandlers.QueryHandlers
{
    public class GetAllPetsHandler : IRequestHandler<GetAllPetsQuery, IEnumerable<PetResponse>>
    {

        private readonly IMapper _mapper;
        private readonly IPetService _petService;

        public GetAllPetsHandler(IMapper mapper, IPetService petService)
        {
            _mapper = mapper;
            _petService = petService;
        }

        public async Task<IEnumerable<PetResponse>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
        {
            var pets = await _petService.GetPetsAsync();
            return _mapper.Map<IEnumerable<PetResponse>>(pets);
        }
    }
}

