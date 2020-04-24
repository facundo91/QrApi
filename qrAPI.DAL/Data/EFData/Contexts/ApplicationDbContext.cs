using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using qrAPI.DAL.Data.EFData.EntityTypeConfigurations;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data.EFData.Contexts
{
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        public ApplicationDbContext()
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<QrDto> Qrs { get; set; }
        public DbSet<PetDto> Pets { get; set; }
        public DbSet<UserPetDto> UserPets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public void HealthCheck() => _ = Database.ProviderName;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=qrdb;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfiguration(new RefreshTokenEntityConfiguration())
                .ApplyConfiguration(new UserPetDtoEntityConfiguration())
                .ApplyConfiguration(new PetDtoEntityConfiguration());
        }


    }
    public class qrContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=qrdb;Trusted_Connection=True");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
