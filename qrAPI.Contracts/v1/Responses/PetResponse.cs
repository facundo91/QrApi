using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Breed { get; set; }
        public IEnumerable<UserResponse> Owners { get; set; } = new List<UserResponse>();
        [Url]
        public string PictureUrl { get; set; }
    }
}
