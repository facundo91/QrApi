using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace qrAPI.Dtos
{
    public abstract class Dto
    {
        [BsonId, Key] public Guid Id { get; set; }

        public Dto()
        {
            Id = Guid.NewGuid();
        }
    }
}