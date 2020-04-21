

using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace qrAPI.DAL.Data.EFData.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Dtos;

    //Should Only operates with DB objects
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
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RefreshToken>().HasKey(x => x.Token);
            modelBuilder.Entity<RefreshToken>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<RefreshToken>().HasAlternateKey(x => x.Id);

            modelBuilder.Entity<PetDto>().Property(pet => pet.PictureUrl)
                .HasConversion(v => v.AbsoluteUri, v => new Uri(v));

            modelBuilder.Entity<PetDto>().Property(pet => pet.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());

            modelBuilder.Entity<UserPetDto>()
                .HasKey(t => new { t.UserId, t.PetId });

            modelBuilder.Entity<UserPetDto>()
                .HasOne(pt => pt.Pet)
                .WithMany(p => p.UserPets)
                .HasForeignKey(pt => pt.PetId);

            modelBuilder.Entity<UserPetDto>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserPets)
                .HasForeignKey(pt => pt.UserId);
        }

    }
}