using AutoMapper;
using qr.Contracts.v1.Requests;
using qr.Domain;

namespace qr.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateQrRequest, Qr>().ReverseMap();
            CreateMap<UpdateQrRequest, Qr>().ReverseMap();
        }
    }
}
