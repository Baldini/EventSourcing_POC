using ES.Domain.Core;
using ES.Domain.Events;
using ES.Domain.Types;
using System;

namespace ES.Domain.Entities
{
    public class Starship : AggregateBase
    {
        private Starship(Guid id) => Id = id.ToString();

        //TODO: Não gostei disso mas por conta da serialização, tem que ter, ver como tirar
        public Starship(string id, DateTime created, long version, string shipName, string currentLocation, Status shipStatus, string pilotName)
        {
            Id = id;
            Created = created;
            Version = version;
            ShipName = shipName;
            CurrentLocation = currentLocation;
            ShipStatus = shipStatus;
            PilotName = pilotName;
        }

        public string ShipName { get; protected set; }
        public string CurrentLocation { get; protected set; }
        public Status ShipStatus { get; protected set; }
        public string PilotName { get; protected set; }

        public static Starship Create(Guid id) => new Starship(id);

        public static Starship CreateStartship(string shipName, string currentLocation, string pilotName)
        {
            var starship = new Starship(Guid.NewGuid());

            var createEvent = StarshipCreate.Create(shipName, currentLocation, pilotName);
            starship.RaiseEvent(createEvent);

            return starship;
        }

        public void LandShip()
        {
            var @event = Land.Create();
            RaiseEvent(@event);
        }

        public void TakeOffShip()
        {
            var @event = TakeOff.Create();
            RaiseEvent(@event);
        }

        public void SetPilot(string pilotName)
        {
            var @event = ChangePilot.Create(pilotName);
            RaiseEvent(@event);
        }

        public void Move(string planetName, int distance)
        {
            var @event = FlyTo.Create(planetName, distance);
            RaiseEvent(@event);
        }

        public void Apply(ChangePilot ev) => PilotName = ev.PilotName;

        public void Apply(FlyTo ev) => CurrentLocation = ev.PlanetName;

        public void Apply(BaseStatus ev) => ShipStatus = ev.ShipStatus;

        public void Apply(StarshipCreate ev)
        {
            ShipName = ev.ShipName;
            PilotName = ev.PilotName;
            CurrentLocation = ev.CurrentLocation;
            ShipStatus = ev.LandStatus;
            Created = DateTime.Now;
        }
    }
}
