using System;
using System.Collections.Generic;
using AutoBogus;
using Bogus.Extensions;

namespace qrAPI.Logic.Domain.Fakers
{
    public sealed class PetFaker : AutoFaker<Pet>
    {
        public PetFaker()
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
            RuleFor(x => x.Birthdate, y => y.Date.Past(12));
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
            RuleFor(x => x.Qr, (_, pet) => new QrFaker(pet).Generate());
            RuleFor(x => x.Owners, (_, pet) => new UserFaker(pet).GenerateBetween(1, 5));
        }

        public PetFaker(Qr qr)
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
            RuleFor(x => x.Birthdate, y => y.Date.Past(12));
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
            RuleFor(x => x.Qr, qr);
            RuleFor(x => x.Owners, (_, pet) => new UserFaker(pet).GenerateBetween(1, 5));
        }

        public PetFaker(User user)
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
            RuleFor(x => x.Birthdate, y => y.Date.Past(12));
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
            RuleFor(x => x.Qr, (_, pet) => new QrFaker(pet).Generate());
            RuleFor(x => x.Owners, y => new List<User> { user });
        }
    }
}
