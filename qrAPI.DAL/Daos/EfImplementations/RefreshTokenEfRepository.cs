using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class RefreshTokenEfRepository : EfRepository<RefreshToken>, IRefreshTokenRepository
    {
        private new readonly DbSet<RefreshToken> _table;

        public RefreshTokenEfRepository(ApplicationDbContext context) : base(context)
        {
            _table = context.Set<RefreshToken>();
        }

        public async Task<RefreshToken> GetByRefreshName(string refreshToken)
        {
            return await _table.SingleOrDefaultAsync(x => x.Token == refreshToken);
        }

    }
}