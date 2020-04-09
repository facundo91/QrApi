using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using qrAPI.Contracts.v1.Requests;

namespace qrAPI.Contracts.v1.Responses
{
    public class PetResponse
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        public Guid? OwnerId { get; set; }
        [Url]
        public string PictureUrl { get; set; }
    }
}
