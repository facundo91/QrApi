using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;

namespace qrAPI.DAL.Data.EFData
{
    //Should Only operates with DB objects
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public IGenericRepository<T> GetRepository<T>() where T : Dto
        {
            return DtosDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IGenericRepository<T>)new GenericEfRepository<QrDto>(this, Qrs),
                1 => (IGenericRepository<T>)new GenericEfRepository<PetDto>(this, Pets),
                2 => (IGenericRepository<T>)new GenericEfRepository<RefreshToken>(this, RefreshTokens),
                _ => throw new InvalidOperationException()
            };
        }

        public DbSet<QrDto> Qrs { get; set; }
        public DbSet<PetDto> Pets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public IRefreshTokenRepository GetRefreshTokenRepository() => new RefreshTokenEfRepository(this, RefreshTokens);

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