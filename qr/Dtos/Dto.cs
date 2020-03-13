using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace qr.Dtos
{
    public abstract class Dto
    {
        [BsonId, Key]
        public Guid Id { get; set; }
    }
}
