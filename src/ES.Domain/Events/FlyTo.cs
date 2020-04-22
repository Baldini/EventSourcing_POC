using ES.Domain.Core;

namespace ES.Domain.Events
{
    public class FlyTo : DomainEventBase
    {
        public override string Name => nameof(FlyTo);

        public string PlanetName { get; private set; }
        public int Distance { get; private set; }

        public FlyTo(string planetName, int distance)
        {
            PlanetName = planetName;
            Distance = distance;
        }

        public static FlyTo Create(string planetName, int distance) => new FlyTo(planetName, distance);

    }
}
