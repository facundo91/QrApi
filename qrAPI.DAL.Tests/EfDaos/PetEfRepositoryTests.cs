

namespace qrAPI.DAL.Tests.EfDaos
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Daos.EfImplementations;
    using Data.EFData.Contexts;
    using Dtos;
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

        /// <summary>
        /// Tests the retrieval of prime numbers from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        //[Fact]
        //public async Task GetPrimesReturnsPrimesFromDatabaseAsync()
        //{
        //    using var database = new TestDatabase();

        //    using (var context = await database.CreateContextAsync().ConfigureAwait(true))
        //    {
        //        UserPetRepository = new UserPetRepository(context);
        //        PetEfRepository = new PetEfRepository(context, UserPetRepository);
        //        //context.PrimeNumbers.AddRange(
        //        //    expectedOutput
        //        //        .Select(x => new PrimeNumber() { Value = x, }));
        //        var pets = await PetEfRepository.GetAllByUserIdAsync(Guid.NewGuid());
        //        pets.Should().BeEmpty();
        //        //await context.SaveChangesAsync().ConfigureAwait(true);
        //    }

        //    using (var context = await database.CreateContextAsync().ConfigureAwait(true))
        //    {
        //        //var manager = this.CreateMathManager(context);
        //        //var primes = await manager.GetPrimesAsync(3).ConfigureAwait(true);

        //        //Assert.Equal(expectedOutput as IEnumerable<long>, primes as IEnumerable<long>);
        //    }
        //}

        [Fact]
        public async Task CreatePetAsyncTest()
        {
            //arrange
            var petDto = new PetDto
            {
                Name = "Test Pet", 
                Birthdate = DateTime.Now, 
                Gender = Gender.Female, 
                PictureUrl = new Uri("https://Test.url")
            };
            var petCreated = await PetEfRepository.InsertAsync(petDto);
            //act
            var pets = await PetEfRepository.GetAllByUserIdAsync(Guid.NewGuid());
            //assert
            pets.Should().BeEmpty();
        }
    }

    public class PetEfRepositoryFixture : IDisposable
    {

        protected PetEfRepository PetEfRepository;
        protected UserPetRepository UserPetRepository;

        protected PetEfRepositoryFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;
           UserPetRepository = new UserPetRepository(new ApplicationDbContext(options));
            PetEfRepository = new PetEfRepository(new ApplicationDbContext(options), UserPetRepository);
        }


        public void Dispose()
        {
        }
    }
}
