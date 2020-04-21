using System.Linq;
using AutoMapper;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<QrDto, Qr>();

            CreateMap<PetDto, Pet>()
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.UserPets.Select(x=>x.User)))
                .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => new Breed{Name = src.Breed}))
                .ForMember(dest => dest.Qr, opt => opt.Ignore());

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.UserPets.Select(x => x.Pet)));
        }
    }
}