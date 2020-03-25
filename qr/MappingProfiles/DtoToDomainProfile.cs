using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

            CreateMap<PetDto, Pet>();

            //CreateMap<PetDto, Pet>();
        }
    }
}