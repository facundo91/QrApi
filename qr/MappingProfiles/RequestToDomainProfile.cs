using AutoMapper;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Domain;

namespace qrAPI.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateQrRequest, Qr>();
            CreateMap<UpdateQrRequest, Qr>();

            CreateMap<CreatePetRequest, Pet>().ForMember(dest => dest.Owner, opt =>
                opt.MapFrom(src => new Person { Id = src.Owner }));
            CreateMap<UpdatePetRequest, Pet>();
        }
    }
}