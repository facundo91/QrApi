using AutoMapper;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Qr, QrDto>().ForMember(dest => dest.PetId, opt =>
                opt.MapFrom(src => src.Pet.Id));

            CreateMap<Pet, PetDto>().ForMember(dest => dest.OwnerId, opt =>
                opt.MapFrom(src => src.Owner.Id)).ForMember(dest => dest.Owner, opt =>
                opt.MapFrom(src => src.Owner.Identity));
        }
    }
}