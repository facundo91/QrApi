using System;
using AutoMapper;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<QrDto, Qr>().ForMember(dest => dest.Pet,
                opt =>
                    opt.MapFrom(src => new Pet { Id = src.PetId }));

            CreateMap<PetDto, Pet>().ForMember(dest => dest.Owner,
                opt =>
                    opt.MapFrom(src => new Person { Id = src.OwnerId.GetValueOrDefault() }));
        }
    }
}