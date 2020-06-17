using System;
using System.Collections.Generic;
using System.Linq;

namespace qrAPI.App.Domain
{
    public class Pet : DomainObject
    {
        public const int MaxAge = 12;
        public const int MaxNameLength = 20;
        public const int MinNameLength = 3;

        public Qr Qr { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public List<User> Owners { get; } = new List<User>();
        public Uri PictureUrl { get; set; }
        public Breed Breed { get; set; }

        public void AddNewOwner(User newOwner, bool bothWays = true)
        {
            if (Owners.Any(owner => owner.Id == newOwner.Id)) return;
            Owners.Add(newOwner);
            if (bothWays) newOwner.AddOwnedPet(this, false);
        }
    }
}
