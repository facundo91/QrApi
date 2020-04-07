using AutoMapper;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters.Interfaces;

namespace qrAPI.Logic.Adapters.Implementations
{
    public class QrServiceAdapter : ServiceAdapter<QrDto>, IQrServiceAdapter
    {
        public QrServiceAdapter(IMapper mapper, IRepository<QrDto> repository) : base(mapper, repository)
        {
        }
    }
}
