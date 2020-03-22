﻿using AutoMapper;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Domain;

namespace qrAPI.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Qr, QrResponse>().ReverseMap();
            CreateMap<Pet, PetResponse>().ReverseMap();
        }
    }
}