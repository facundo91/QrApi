using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace qrAPI.Logic.Domain
{
    public class Person : DomainObject
    {
        public IdentityUser Identity { get; set; }
        public IEnumerable<Pet> Pets { get; set; } = new List<Pet>();

        public void AddOwnedPet(Pet pet, bool bothWays = true)
        {
            if (Pets.ToList().Exists(x => x.Id == pet.Id)) return;
            Pets.ToList().Add(pet);
            if (bothWays) pet.AddNewOwner(this, false);
        }
    }
}
