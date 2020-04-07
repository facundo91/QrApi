#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace qrAPI.DAL.Dtos
{
    [Table("Pets")]
    public class PetDto : Dto
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public Guid? OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public virtual IdentityUser Owner { get; set; }
        public string PictureUrl { get; set; }
    }
}
