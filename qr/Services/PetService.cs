using qrAPI.Domain;
using qrAPI.Dtos;
using qrAPI.Mediators;

namespace qrAPI.Services
{
    public class PetService : AbstractGenericService<Pet, PetDto>, IPetService<Pet> 
    {
        public PetService(IServiceDalMediator<Pet, PetDto> serviceDalMediator) : base(serviceDalMediator)
        {
        }

    }
}