using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    public abstract class Dto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        //[BsonId]
        [Key]
        public virtual Guid Id { get; set; }
    }
}
