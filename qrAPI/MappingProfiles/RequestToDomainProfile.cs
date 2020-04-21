using System.Linq;
using AutoMapper;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateQrRequest, Qr>()
                .ForMember(dest => dest.Pet, opt => opt.MapFrom(src => new Pet { Id = src.PetId }))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateQrRequest, Qr>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Pet, opt => opt.Ignore());

            CreateMap<CreatePetRequest, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Qr, opt => opt.Ignore())
                .ForMember(dest => dest.Owners, opt => opt.Ignore())
                .ForMember(dest => dest.Breed, opt => opt.MapFrom(x => new Breed { Name = x.Breed }));

            CreateMap<UpdatePetRequest, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Qr, opt => opt.Ignore())
                .ForMember(dest => dest.Breed, opt => opt.MapFrom(x => new Breed { Name = x.Breed }))
                .ForMember(dest => dest.Owners, opt =>
                opt.MapFrom(src => src.Owners.Select(x => new User { Id = x })));
        }
    }
}