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
            QrRepository = new GenericEfRepository<QrDto>(this);
        }

        public IGenericRepository<QrDto> QrRepository { get; }
        public void HealthCheck() => _ = Database.ProviderName;
    }
}