using AutoBogus;

namespace qrAPI.DAL.Dtos.Fakers
{
    public sealed class UserPetDtoFaker : AutoFaker<UserPetDto>
    {
        public UserPetDtoFaker(PetDto petDto, UserDto userDto)
        {
            RuleFor(x => x.Pet, petDto);
            RuleFor(x => x.PetId, petDto.Id);
            RuleFor(x => x.User, userDto);
            RuleFor(x => x.UserId, userDto.Id);
        }
    }
}
