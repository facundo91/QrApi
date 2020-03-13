using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qr.DAL;
using qr.Domain;
using qr.Dtos;

namespace qr.Data.EFData
{
    //Should Only operates with DB objects
    public class ApplicationDbContext : IdentityDbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMapper mapper)
            : base(options)
        {
            qrRepository = new GenericEfRepository<QrDto, Qr>(this, mapper);
        }

        public IGenericRepository<QrDto, Qr> qrRepository { get; }
        public void HealthCheck() => _ = Database.ProviderName;
    }
}
