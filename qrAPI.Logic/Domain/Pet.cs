using System;
using System.Collections.Generic;

namespace qrAPI.Logic.Domain
{
    public class Pet : DomainObject
    {
        public Qr Qr { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public Person? Owner { get; set; }
        public Pet Dad { get; set; }
        public Pet Mom { get; set; }
        public Uri PictureUrl { get; set; }
        public Breed Breed { get; set; }
        public IEnumerable<MedicalRecord> MedicalRecords { get; set; }

        public void AddNewOwner(Person newOwner, bool bothWays = true)
        {
            if (Owner.Id == newOwner.Id) return;
            Owner = newOwner;
            if (bothWays) newOwner.AddOwnedPet(this,false);
        }
    }
}
