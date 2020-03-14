using AutoMapper;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Domain;

namespace qrAPI.MappingProfiles
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