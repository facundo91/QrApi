using AutoMapper;
using qrAPI.App.Domain;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Qr, QrResponse>();

            CreateMap<Pet, PetResponse>()
                .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed.Name))
                .ForMember(dest => dest.Owners, opt => opt.MapFrom(src => src.Owners));

            CreateMap<User, UserResponse>();
        }
    }
}