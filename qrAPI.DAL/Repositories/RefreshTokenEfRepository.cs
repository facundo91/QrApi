using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Repositories
{
    public class RefreshTokenEfRepository : GenericEfRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly DbSet<RefreshToken> _table;

        public RefreshTokenEfRepository(ApplicationDbContext context, DbSet<RefreshToken> table) : base(context, table)
        {
            _table = table;
        }

        public async Task<RefreshToken> GetByRefreshName(string refreshToken)
        {
            return await _table.SingleOrDefaultAsync(x => x.Token == refreshToken);
        }

    }
}