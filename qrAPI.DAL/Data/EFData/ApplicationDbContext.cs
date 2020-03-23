using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;

namespace qrAPI.DAL.Data.EFData
{
    //Should Only operates with DB objects
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        public IGenericRepository<T> GetRepository<T>() where T : Dto
        {
            return DtosDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IGenericRepository<T>)new GenericEfRepository<QrDto>(this),
                1 => (IGenericRepository<T>)new GenericEfRepository<PetDto>(this),
                _ => throw new InvalidOperationException()
            };
        }
        public void HealthCheck() => _ = Database.ProviderName;
    }
}