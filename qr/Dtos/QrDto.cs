using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace qr.Dtos
{
    public class QrDto
    {
        [BsonId]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
