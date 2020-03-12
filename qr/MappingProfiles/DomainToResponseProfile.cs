using AutoMapper;
using qr.Contracts.v1.Responses;
using qr.Domain;

namespace qr.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Qr, QrResponse>().ReverseMap();
        }
    }
}
