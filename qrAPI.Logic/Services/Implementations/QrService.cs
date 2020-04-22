using System;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Mail;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService
    {
        private readonly IMailService _mailService;
        private readonly IPetService _petService;
        private readonly IIdentityService _identityService;

        public QrService(IQrServiceAdapter serviceToDalAdapter, IMailService mailService, IPetService petService, IIdentityService identityService) : base(serviceToDalAdapter)
        {
            _mailService = mailService;
            _petService = petService;
            _identityService = identityService;
        }

        public async Task ScanQr(Guid qrId)
        {
            var qr = await _serviceToDalAdapter.GetByIdAsync<Qr>(qrId);
            var pet = await _petService.GetByIdAsync(qr.Pet.Id);
            foreach (var owner in pet.Owners)
            {
                var identity = await _identityService.GetPersonAsync(owner.Id);
                await _mailService.SendSimpleMessage("", identity.Identity.Email, $"{pet.Name} has been scanned",
                    $"Dear {identity.Identity.UserName}, please do something!");
            }
        }
    }
}