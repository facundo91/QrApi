using System;
using System.ComponentModel.DataAnnotations;

namespace qrAPI.Contracts.v1.Requests.Create
{
    public class CreatePetRequest
    {
        [Required]
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public Guid OwnerId { get; set; }
        [Url]
        public string PictureUrl { get; set; }
    }
}
