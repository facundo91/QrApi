using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    [Table("Qrs")]
    public class QrDto : Dto
    {
        public string Name { get; set; }

        public Guid PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        [NotMapped]
        public virtual PetDto Pet { get; set; }
    }
}