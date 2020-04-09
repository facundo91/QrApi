using System;
using qrAPI.DAL.Daos.EfImplementations;

namespace qrAPI.DAL.Tests.EfDaos
{
    public class PetEfRepositoryTests : PetEfRepositoryFixture
    {
        //[Fact]
        //public async Task GetAllByUserIdAsyncTest()
        //{
        //    //arrange
        //    //act
        //    var pets = await PetEfRepository.GetAllByUserIdAsync("id-123");
        //    //assert
        //    pets.Should().HaveCount(3);
        //}
    }

    public class PetEfRepositoryFixture : IDisposable
    {

        protected PetEfRepository PetEfRepository;

        public PetEfRepositoryFixture()
        {

            PetEfRepository = new PetEfRepository(null);
        }


        public void Dispose()
        {
        }
    }
}
