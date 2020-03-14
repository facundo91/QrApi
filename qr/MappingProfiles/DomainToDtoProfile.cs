using AutoMapper;
using qrAPI.DAL.Dtos;
using qrAPI.Domain;

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