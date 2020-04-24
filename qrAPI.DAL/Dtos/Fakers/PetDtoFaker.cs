using System;
using AutoBogus;

namespace qrAPI.DAL.Dtos.Fakers
{

    public sealed class PetDtoFaker : AutoFaker<PetDto>
    {
        public PetDtoFaker()
        {
            RuleFor(x => x.Name, y => y.Name.FirstName());
            RuleFor(x => x.Birthdate, y => y.Date.Past(15));
            RuleFor(x => x.Breed, y => y.Lorem.Word());
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
        }
    }
}
