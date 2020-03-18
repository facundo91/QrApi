using AutoMapper;
using qrAPI.Domain;
using qrAPI.Dtos;

namespace qrAPI.MappingProfiles
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Qr, QrDto>().ReverseMap();
        }
    }
}