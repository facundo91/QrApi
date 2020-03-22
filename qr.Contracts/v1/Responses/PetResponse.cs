using System;
using qrAPI.Contracts.v1.Requests;

namespace qrAPI.Contracts.v1.Responses
{
    public class PetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public CreatedPersonResponse Owner { get; set; }
        public string PictureUrl { get; set; }
    }

    public class CreatedPersonResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
