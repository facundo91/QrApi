using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Mediators;

namespace qrAPI.Logic.Services
{
    public class PetService : AbstractGenericService<Pet, PetDto>, IPetService<Pet> 
    {
        public PetService(IServiceDalMediator<Pet, PetDto> serviceDalMediator) : base(serviceDalMediator)
        {
        }

    }
}