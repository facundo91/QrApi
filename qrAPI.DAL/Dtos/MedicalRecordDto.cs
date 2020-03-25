using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.DAL.Dtos
{
    public class MedicalRecordDto : Dto
    {
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public Guid PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        public virtual PetDto Pet { get; set; }
    }
}
