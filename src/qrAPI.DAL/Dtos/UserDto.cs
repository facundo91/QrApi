using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    [Table("Users")]
    public class UserDto : Dto
    {
        public IdentityUser Identity { get; set; }
        public IEnumerable<UserPetDto> UserPets { get; set; } = new List<UserPetDto>();
    }
}
