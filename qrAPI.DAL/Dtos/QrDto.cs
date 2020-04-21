using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    [Table("Qrs")]
    public class QrDto : Dto
    {
        [Required]
        public string Name { get; set; }
        public Guid PetId { get; set; }
        [ForeignKey("PetId")]
        public PetDto Pet { get; set; }
    }
}