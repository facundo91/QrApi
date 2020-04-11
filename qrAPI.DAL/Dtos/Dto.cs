using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace qrAPI.DAL.Dtos
{
    public abstract class Dto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[BsonId]
        [Key]
        public virtual Guid Id { get; set; }
    }
}
