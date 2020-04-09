using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qrAPI.Contracts.v1.Responses
{
    public class QrResponse
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(PetResponse))]
        public Guid PetId { get; set; }
        public virtual PetResponse Pet { get; set; }
    }
}