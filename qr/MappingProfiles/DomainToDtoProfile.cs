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

            CreateMap<Pet, PetDto>();
        }
    }
}