using AutoBogus;

namespace qrAPI.Logic.Domain.Fakers
{
    public sealed class QrFaker : AutoFaker<Qr>
    {
        public QrFaker()
        {
            RuleFor(x => x.Name, y => y.Name.FirstName());
            RuleFor(x => x.Pet, (petFaker,qr )=>new PetFaker(qr).Generate());
        }

        public QrFaker(Pet pet)
        {
            RuleFor(x => x.Name, y => y.Name.FirstName());
            RuleFor(x => x.Pet, pet);
        }
    }
}