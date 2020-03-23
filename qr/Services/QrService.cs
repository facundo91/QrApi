﻿using qrAPI.DAL.Dtos;
using qrAPI.Domain;
using qrAPI.Mediators;

namespace qrAPI.Services
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService<Qr>
    {
        public QrService(IServiceDalMediator<Qr, QrDto> serviceDalMediator) : base(serviceDalMediator)
        {
        }
    }
}