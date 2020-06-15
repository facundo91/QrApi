using AutoBogus;

namespace qrAPI.DAL.Dtos.Fakers
{
    public sealed class QrDtoFaker : AutoFaker<QrDto>
    {
        public QrDtoFaker()
        {
            RuleFor(x => x.Name, y => y.Lorem.Word());
        }
    }
}