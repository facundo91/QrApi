using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class PetsControllerAdapter : ControllerAdapter<Pet>,  IPetsControllerAdapter
    {
        private new readonly IPetService _service;

        public PetsControllerAdapter(IMapper mapper, IPetService service) : base(mapper, service)
        {
            _service = service;
        }
    }
}
