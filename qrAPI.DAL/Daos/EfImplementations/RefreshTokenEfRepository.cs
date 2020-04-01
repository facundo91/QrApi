using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class RefreshTokenEfRepository : EfRepository<RefreshToken>, IRefreshTokenRepository
    {

        public RefreshTokenEfRepository(ApplicationDbContext context, DbSet<RefreshToken> table) : base(context)
        {
        }

        public async Task<RefreshToken> GetByRefreshName(string refreshToken)
        {
            return await _table.SingleOrDefaultAsync(x => x.Token == refreshToken);
        }

    }
}