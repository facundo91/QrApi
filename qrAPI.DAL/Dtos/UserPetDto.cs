using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    [Table("UserPets")]
    public class UserPetDto : Dto
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public Guid PetId { get; set; }
        public PetDto Pet { get; set; }
    }
}
