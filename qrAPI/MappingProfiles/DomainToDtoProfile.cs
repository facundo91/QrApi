using AutoMapper;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.MappingProfiles
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Qr, QrDto>().ForMember(dest => dest.PetId, opt =>
                opt.MapFrom(src => src.Pet.Id));

            // https://entityframeworkcore.com/knowledge-base/49207534/automapper--one-to-many----many-to-many
            CreateMap<Pet, PetDto>()
                .ForMember(entity => entity.Breed,
                    opt => opt.MapFrom(model => model.Breed.Name))
                // (1)
                .ForMember(entity => entity.UserPets, 
                    opt => opt.MapFrom(model => model.Owners))
                // (5)
                .AfterMap((model, entity) =>
                {
                    foreach (var entityUserAndTag in entity.UserPets)
                    {
                        entityUserAndTag.Pet = entity;
                        entityUserAndTag.PetId = entity.Id;
                    }
                });

            // (2)
            CreateMap<User, UserPetDto>()
                // (3)
                .ForMember(entity => entity.User, opt => opt.MapFrom(model => model))
                .ForMember(x => x.UserId, opt => opt.MapFrom(model => model.Id))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.PetId, opt => opt.Ignore())
                .ForMember(x => x.Pet, opt => opt.Ignore());

            // (4)
            CreateMap<User, UserDto>()
                .ForMember(x=> x.UserPets,opt => opt.Ignore());
        }
    }
}