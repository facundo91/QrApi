using System;
using System.ComponentModel.DataAnnotations;

namespace qrAPI.Contracts.v1.Requests.Create
{
    public class CreatePetRequest
    {
        [Required]
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Url]
        public string PictureUrl { get; set; }
        public string Breed { get; set; }

    }
}
