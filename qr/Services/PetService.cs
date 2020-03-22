using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using qrAPI.Commands.Pets.ServiceCommands;
using qrAPI.Domain;
using qrAPI.Queries.Pets.ServiceQueries;

namespace qrAPI.Services
{
    public class PetService : IPetService
    {
        private readonly IMediator _mediator;

        public PetService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IEnumerable<Pet>> GetPetsAsync()
        {
            var pet = new GetPetsAsyncQuery();
            var result = await _mediator.Send(pet);
            return result;
        }

        public async Task<Pet> GetPetByIdAsync(Guid petId)
        {
            throw new NotImplementedException();
        }

        public async Task<Pet> CreatePetAsync(Pet petToCreate)
        {
            var command = new CreatePetAsyncCommand(petToCreate);
            Pet result = await _mediator.Send(command);
            return result;
        }

        public async Task<bool> UpdatePetAsync(Pet petToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePetAsync(Guid petId)
        {
            throw new NotImplementedException();
        }
    }
}