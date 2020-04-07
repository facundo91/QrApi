using AutoMapper;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class QrsControllerAdapter : ControllerAdapter<Qr>, IQrsControllerAdapter
    {
        public QrsControllerAdapter(IMapper mapper, IQrService service) : base(mapper, service)
        {
        }
    }
}
