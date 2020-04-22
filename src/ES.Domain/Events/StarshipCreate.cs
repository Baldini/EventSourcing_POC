using ES.Domain.Core;
using ES.Domain.Types;

namespace ES.Domain.Events
{
    public class StarshipCreate : DomainEventBase
    {

        public override string Name => nameof(StarshipCreate);

        public StarshipCreate(string shipName, string currentLocation, string pilotName)
        {
            ShipName = shipName;
            CurrentLocation = currentLocation;
            PilotName = pilotName;
            LandStatus = Status.Landed;
        }

        public static StarshipCreate Create(string shipName,
                                             string currentLocation,
                                             string pilotName) =>
            new StarshipCreate(
                shipName,
                currentLocation,
                pilotName);

        public string ShipName { get; private set; }
        public string CurrentLocation { get; private set; }
        public Status LandStatus { get; private set; }
        public string PilotName { get; private set; }
    }
}
