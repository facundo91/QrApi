using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters.Implementations
{
    public class PetServiceAdapter : ServiceAdapter<PetDto>, IPetServiceAdapter
    {
        private new readonly IPetRepository _repository;

        public PetServiceAdapter(IMapper mapper, IPetRepository repository) : base(mapper, repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pet>> GetAllByUserAsync(Guid userId)
        {
            var petsDto = await _repository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<Pet>>(petsDto);
        }
    }
}