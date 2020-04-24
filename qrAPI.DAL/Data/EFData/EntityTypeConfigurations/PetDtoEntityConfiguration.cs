using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data.EFData.EntityTypeConfigurations
{
   public class PetDtoEntityConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.Property(pet => pet.PictureUrl)
                .HasConversion(v => v.AbsoluteUri, v => new Uri(v));

            builder.Property(pet => pet.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());
        }
    }
}
