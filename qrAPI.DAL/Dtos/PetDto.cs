using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    [Table("Pets")]
    public class PetDto : Dto
    {
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string Breed { get; set; }
        public IEnumerable<UserPetDto> UserPets { get; set; } = new List<UserPetDto>();
        public Uri PictureUrl { get; set; }
    }
}
