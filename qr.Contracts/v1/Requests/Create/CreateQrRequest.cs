using System;
using System.ComponentModel.DataAnnotations;

namespace qrAPI.Contracts.v1.Requests.Create
{
    public class CreateQrRequest
    {
        [Required]
        public string Name { get; set; }
        public Guid PetId { get; set; }
    }
}