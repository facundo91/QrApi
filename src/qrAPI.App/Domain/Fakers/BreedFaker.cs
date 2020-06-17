using AutoBogus;

namespace qrAPI.App.Domain.Fakers
{
    public sealed class BreedFaker : AutoFaker<Breed>
    {
        public BreedFaker()
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
        }
    }
}