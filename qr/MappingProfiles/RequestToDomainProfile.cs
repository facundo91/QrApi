using AutoMapper;
using Microsoft.AspNetCore.Identity;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateQrRequest, Qr>().ForMember(dest => dest.Pet, opt =>
                opt.MapFrom(src => new Pet { Id = src.PetId }));

            CreateMap<UpdateQrRequest, Qr>();

            CreateMap<CreatePetRequest, Pet>();

            CreateMap<UpdatePetRequest, Pet>();
        }
    }
}