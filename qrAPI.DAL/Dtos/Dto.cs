using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace qrAPI.DAL.Dtos
{
    public abstract class Dto
    {
        [BsonId, Key] public Guid Id { get; set; }
    }
}