using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.CacheImplementations
{
    public class PetCacheRepository : CacheRepository<PetDto>, IPetRepository
    {
        public PetCacheRepository(IPetRepository repository) : base(repository)
        {
        }

        public Task<IEnumerable<PetDto>> GetAllByName(string name)
        {
            //use cache function to try to get the value
            //pass as parameter the way to get it from the repository
            throw new NotImplementedException();
        }
    }
}
