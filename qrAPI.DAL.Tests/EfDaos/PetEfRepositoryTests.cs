using qrAPI.DAL.Dtos.Fakers;

namespace qrAPI.DAL.Tests.EfDaos
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Daos.EfImplementations;
    using Data.EFData.Contexts;
    using Xunit;
    public class PetEfRepositoryTests : PetEfRepositoryFixture
    {
        [Fact]
        public async Task GetAllByUserIdAsyncTest()
        {
            //arrange
            //act
            var pets = await PetEfRepository.GetAllByUserIdAsync(Guid.NewGuid());
            //assert
            pets.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePetAsyncTest()
        {
            //arrange
            var petDto = new PetDtoFaker().Generate();
            await PetEfRepository.InsertAsync(petDto);
            //act
            var pets = await PetEfRepository.GetAllByUserIdAsync(Guid.NewGuid());
            //assert
            pets.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePetAsyncShouldExistTest()
        {
            //arrange
            var petDto = new PetDtoFaker().Generate();
            var petCreated = await PetEfRepository.InsertAsync(petDto);
            //act
            var pets = await PetEfRepository.GetAsync(petCreated.Id);
            //assert
            pets.Should().BeEquivalentTo(petDto);
        }
    }

    public class PetEfRepositoryFixture : IDisposable
    {

        protected readonly PetEfRepository PetEfRepository;
        private readonly ApplicationDbContext _dbContext;

        protected PetEfRepositoryFixture()
        {
            _dbContext = new qrContextFactory().CreateDbContext(new string[] { });
            _dbContext.Database.EnsureCreated();
            var userPetRepository = new UserPetRepository(_dbContext);
            PetEfRepository = new PetEfRepository(_dbContext, userPetRepository);
        }


        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
