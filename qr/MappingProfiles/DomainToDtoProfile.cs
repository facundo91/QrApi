using AutoMapper;
using qr.Domain;
using qr.Dtos;

namespace qr.MappingProfiles
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Qr, QrDto>().ReverseMap();
        }
    }
}
