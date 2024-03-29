﻿using AutoBogus;
using Bogus.Extensions;
using System.Collections.Generic;

namespace qrAPI.App.Domain.Fakers
{
    public sealed class UserFaker : AutoFaker<User>
    {
        public UserFaker()
        {
            RuleFor(x => x.Pets, (_, user) => new PetFaker(user).GenerateBetween(1, 5));
        }

        public UserFaker(Pet pet)
        {
            RuleFor(x => x.Pets, (_, user) => new List<Pet> { pet });
        }
    }
}
