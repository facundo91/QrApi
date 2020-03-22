using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using qrAPI.Data;
using qrAPI.Domain;
using qrAPI.Dtos;
using qrAPI.Queries.Pets.ServiceQueries;
using qrAPI.Repositories;

namespace qrAPI.Handlers.Pets.ServiceHandlers.QueryHandlers
{
    public class GetPetsAsyncHandler : IRequestHandler<GetPetsAsyncQuery, IEnumerable<Pet>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<PetDto> _petRepository;

        public GetPetsAsyncHandler(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _petRepository = dataContext.PetRepository;
        }

        public async Task<IEnumerable<Pet>> Handle(GetPetsAsyncQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Pet>>(await _petRepository.GetAllAsync());
        }
    }
}