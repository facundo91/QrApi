using AutoMapper;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class PetsControllerAdapter : ControllerAdapter<Pet>,  IPetsControllerAdapter
    {
        public PetsControllerAdapter(IMapper mapper, IPetService service) : base(mapper, service)
        {
        }
    }
}
