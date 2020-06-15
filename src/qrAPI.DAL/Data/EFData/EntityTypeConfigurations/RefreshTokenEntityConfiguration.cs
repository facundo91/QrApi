using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data.EFData.EntityTypeConfigurations
{
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Token);
            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            builder.HasAlternateKey(x => x.Id);
        }
    }
}
