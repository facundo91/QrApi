using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data.EFData.EntityTypeConfigurations
{
    public class UserPetDtoEntityConfiguration: IEntityTypeConfiguration<UserPetDto>
    {
        public void Configure(EntityTypeBuilder<UserPetDto> builder)
        {
            builder
                .HasKey(t => new { t.UserId, t.PetId });

            builder
                .HasOne(pt => pt.Pet)
                .WithMany(p => p.UserPets)
                .HasForeignKey(pt => pt.PetId);

            builder
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserPets)
                .HasForeignKey(pt => pt.UserId);
        }
    }
}
