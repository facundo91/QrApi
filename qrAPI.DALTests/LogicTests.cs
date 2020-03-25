using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Tests
{
    public class LogicTests
    {
        protected readonly Person FacundoPerson = new Person
        {
            Id = Guid.NewGuid(),
            Identity = new IdentityUser("Facundo"),
            Pets = new List<Pet>()
        };
        protected readonly Person OctavioPerson = new Person
        {
            Id = Guid.NewGuid(),
            Identity = new IdentityUser("Octavio"),
            Pets = new List<Pet>()
        };
        protected readonly Pet PampaPet = new Pet
        {
            Birthdate = DateTime.Now,
            Breed = new Breed { Id = Guid.NewGuid(), Name = "Pitbull" },
            Id = Guid.NewGuid(),
            Name = "Pampa",
            Qr = null,
            Dad = null,
            Gender = Gender.Female,
            Mom = null,
            Owners = new List<Person>(),
            MedicalRecords = new List<MedicalRecord>()
        };
        protected readonly Pet VulpiPet = new Pet
        {
            Birthdate = DateTime.Now,
            Breed = new Breed { Id = Guid.NewGuid(), Name = "Chihuahua" },
            Id = Guid.NewGuid(),
            Name = "Vulpi",
            Qr = null,
            Dad = null,
            Gender = Gender.Male,
            Mom = null,
            Owners = new List<Person>(),
            MedicalRecords = new List<MedicalRecord>()
        };
    }
}
