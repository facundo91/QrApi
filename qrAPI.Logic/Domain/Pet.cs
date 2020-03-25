using System;
using System.Collections.Generic;
using System.Linq;

namespace qrAPI.Logic.Domain
{
    public class Pet : DomainObject
    {
        public Qr Qr { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public IEnumerable<Person> Owners { get; set; } = new List<Person>();
        public Pet Dad { get; set; }
        public Pet Mom { get; set; }
        public Uri PictureUrl { get; set; }
        public Breed Breed { get; set; }
        public IEnumerable<MedicalRecord> MedicalRecords { get; set; }

        public void AddNewOwner(Person newOwner, bool bothWays = true)
        {
            if (Owners.ToList().Exists(x => x.Id == newOwner.Id)) return;
            Owners.ToList().Add(newOwner);
            if (bothWays) newOwner.AddOwnedPet(this,false);
        }
    }
}
