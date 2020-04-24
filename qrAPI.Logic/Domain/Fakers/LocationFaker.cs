using AutoBogus;

namespace qrAPI.Logic.Domain.Fakers
{
    public sealed class LocationFaker : AutoFaker<Location>
    {
        public LocationFaker()
        {
            RuleFor(x => x.Latitude, y => y.Address.Latitude());
            RuleFor(x => x.Longitude, y => y.Address.Longitude());
        }
    }
}