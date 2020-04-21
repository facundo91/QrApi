using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace qrAPI.DAL.Dtos
{
    [Table("Users")]
    public class UserDto : Dto
    {
        public IdentityUser Identity { get; set; }
        public IEnumerable<UserPetDto> UserPets { get; set; } = new List<UserPetDto>();
    }
}
