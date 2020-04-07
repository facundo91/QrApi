using AutoMapper;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Qr, QrResponse>();

            CreateMap<Pet, PetResponse>().ForMember(dest => dest.Owner,
                opt =>
                    opt.MapFrom(src => src.Owner.Id));
        }
    }
}