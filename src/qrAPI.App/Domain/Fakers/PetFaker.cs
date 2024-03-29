﻿using AutoBogus;
using Bogus.Extensions;
using System;
using System.Collections.Generic;

namespace qrAPI.App.Domain.Fakers
{
    public sealed class PetFaker : AutoFaker<Pet>
    {
        public PetFaker()
        {
            RuleFor(x => x.Name, y => y.Name.FirstName());
            RuleFor(x => x.Birthdate, y => y.Date.Past(Pet.MaxAge));
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
            RuleFor(x => x.Qr, (_, pet) => new QrFaker(pet).Generate());
            RuleFor(x => x.Owners, (_, pet) => new UserFaker(pet).GenerateBetween(1, 5));
        }

        public PetFaker(Qr qr)
        {
            RuleFor(x => x.Name, y => y.Name.FirstName());
            RuleFor(x => x.Birthdate, y => y.Date.Past(Pet.MaxAge));
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
            RuleFor(x => x.Qr, qr);
            RuleFor(x => x.Owners, (_, pet) => new UserFaker(pet).GenerateBetween(1, 5));
        }

        public PetFaker(User user)
        {
            RuleFor(x => x.Name, y => y.Name.FirstName());
            RuleFor(x => x.Birthdate, y => y.Date.Past(Pet.MaxAge));
            RuleFor(x => x.PictureUrl, y => new Uri(y.Internet.Avatar()));
            RuleFor(x => x.Qr, (_, pet) => new QrFaker(pet).Generate());
            RuleFor(x => x.Owners, y => new List<User> { user });
        }
    }
}
