using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public class PetService : AbstractGenericService<Pet, PetDto>, IPetService 
    {
        public PetService(IServiceAdapter<Pet, PetDto> serviceToDalAdapter) : base(serviceToDalAdapter)
        {
        }

    }
}