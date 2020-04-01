using System;

namespace qrAPI.Contracts.v2.Requests.Create
{
    public class CreatePetRequest
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public Guid OwnerId { get; set; }
        public string PictureUrl { get; set; }
    }
}
