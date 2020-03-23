using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace qrAPI.DAL.Dtos
{
    public class Dto
    {
        [BsonId, Key] public Guid Id { get; set; }
    }
}
