using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Mail;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService
    {
        private readonly IMailService _mailService;
        private readonly IGenericService<Pet> _petService;
        private readonly IIdentityService _identityService;

        public QrService(
            IMapper mapper, 
            IRepository<QrDto> repository, 
            IMailService mailService, 
            IGenericService<Pet> petService, 
            IIdentityService identityService) 
            : base(mapper, repository)
        {
            _mailService = mailService;
            _petService = petService;
            _identityService = identityService;
        }

        public async Task ScanQr(Guid qrId)
        {
            var qr = await GetByIdAsync(qrId);
            var pet = await _petService.GetByIdAsync(qr.Pet.Id);
            foreach (var owner in pet.Owners)
            {
                var identity = await _identityService.GetPersonAsync(owner.Id);
                await _mailService.SendSimpleMessage("facundo@qrightthing.tech", identity.Identity.Email, $"{pet.Name} has been scanned",
                    $"Dear {identity.Identity.UserName}, please do something!");
            }
        }


    }
}