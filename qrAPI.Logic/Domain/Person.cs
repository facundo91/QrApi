using System;
using System.Collections.Generic;

namespace qrAPI.Logic.Domain
{
    public class Person : DomainObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public IEnumerable<Pet> Pets { get; set; }
    }
}
