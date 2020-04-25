using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class RefreshTokenEfRepository : EfRepository<RefreshToken>, IRefreshTokenRepository
    {

        public RefreshTokenEfRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetByRefreshName(string refreshToken) => 
            await Table.SingleOrDefaultAsync(x => x.Token == refreshToken);
    }
}