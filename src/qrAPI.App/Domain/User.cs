using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace qrAPI.App.Domain
{
    public class User : DomainObject
    {
        public IdentityUser Identity { get; set; }
        public List<Pet> Pets { get; } = new List<Pet>();

        public void AddOwnedPet(Pet pet, bool bothWays = true)
        {
            if (Pets.ToList().Exists(x => x.Id == pet.Id)) return;
            Pets.Add(pet);
            if (bothWays) pet.AddNewOwner(this, false);
        }
    }
}
