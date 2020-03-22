using System;

namespace qrAPI.Contracts.v1.Requests
{
    public class UpdatePetRequest
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public Guid Owner { get; set; }
        public string PictureUrl { get; set; }
    }
}
