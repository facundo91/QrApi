using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;
using System.Threading.Tasks;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class QrEfRepository : EfRepository<QrDto>
    {
        public QrEfRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<QrDto> InsertAsync(QrDto obj)
        {
            obj.Pet = null;
            return await base.InsertAsync(obj);
        }

        public override async Task<QrDto> GetAsync(object id) =>
            await GetAsyncIncludeProperty(id, x => x.Pet);
    }
}
