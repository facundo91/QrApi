using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Data.EFData
{
    //Should Only operates with DB objects
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            QrRepository = new GenericEfRepository<QrDto>(this);
            PetRepository = new GenericEfRepository<PetDto>(this);
        }

        public IGenericRepository<QrDto> QrRepository { get; }
        public IGenericRepository<PetDto> PetRepository { get; }
        public void HealthCheck() => _ = Database.ProviderName;
    }
}