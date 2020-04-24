using AutoBogus;

namespace qrAPI.Logic.Domain.Fakers
{
    public sealed class BreedFaker : AutoFaker<Breed>
    {
        public BreedFaker()
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
        }
    }
}