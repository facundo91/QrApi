using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data.EFData.Contexts
{
    //Should Only operates with DB objects
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<QrDto> Qrs { get; set; }
        public DbSet<PetDto> Pets { get; set; }
        public DbSet<MedicalRecordDto> MedicalRecords { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public void HealthCheck() => _ = Database.ProviderName;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RefreshToken>().HasKey(x => x.Token);
            modelBuilder.Entity<RefreshToken>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<RefreshToken>().HasAlternateKey(x => x.Id);
            modelBuilder.Entity<PetDto>().Ignore(x => x.Owner);
        }

    }
}