using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using qrAPI.DAL.Daos.EfImplementations;
using qrAPI.DAL.Daos.JsonImplementations;
using qrAPI.DAL.Daos.MongoImplementations;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.Interfaces
{
    public interface IPetRepository : IRepository<PetDto>
    {
        Task<IEnumerable<PetDto>> GetAllByName(string name);
    }

    public class PetJsonRepository : JsonRepository<PetDto>, IPetRepository
    {
        public PetJsonRepository(string path) : base(path)
        {
        }

        public Task<IEnumerable<PetDto>> GetAllByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }

    public class PetMongoRepository : MongoRepository<PetDto>, IPetRepository
    {
        public PetMongoRepository(IMongoCollection<PetDto> table) : base(table)
        {
        }

        public Task<IEnumerable<PetDto>> GetAllByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }

    public class PetEfRepository : EfRepository<PetDto>, IPetRepository
    {
        public PetEfRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<PetDto>> GetAllByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
