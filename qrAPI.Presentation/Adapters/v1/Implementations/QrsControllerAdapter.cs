using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class QrsControllerAdapter : ControllerAdapter<Qr>, IQrsControllerAdapter
    {
        private readonly IQrService _service;

        public QrsControllerAdapter(IMapper mapper, IQrService service) : base(mapper, service)
        {
            _service = service;
        }

        public async Task ScanQr(Guid qrId)
        {
            await _service.ScanQr(qrId);
        }
    }
}
