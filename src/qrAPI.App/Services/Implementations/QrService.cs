using AutoMapper;
using Microsoft.Extensions.Logging;
using qrAPI.App.Domain;
using qrAPI.App.Domain.Validators;
using qrAPI.App.Services.Interfaces;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Infra.Mail;
using qrAPI.Infra.Mail.Models;
using System;
using System.Threading.Tasks;

namespace qrAPI.App.Services.Implementations
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService
    {
        private readonly IMailBroker _mailService;
        private readonly IGenericService<Pet> _petService;
        private readonly IIdentityService _identityService;

        public QrService(
            IMapper mapper,
            IRepository<QrDto> repository,
            IMailBroker mailService,
            IGenericService<Pet> petService,
            IIdentityService identityService,
            ILogger<QrService> logger)
            : base(mapper, repository, logger)
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
                var mail = new Mail("facundo@qrightthing.tech", identity.Identity.Email, $"{pet.Name} has been scanned",
                    $"Dear {identity.Identity.UserName}, please do something!");
                await _mailService.SendSimpleMessage(mail);
            }
        }

    }
}