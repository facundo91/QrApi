using System;
using System.ComponentModel.DataAnnotations;

namespace qrAPI.Contracts.v1.Requests.Update
{
    public class UpdatePetRequest
    {
        [Required]
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        public Guid OwnerId { get; set; }
        public string PictureUrl { get; set; }
    }
}
