using ES.Domain.Entities;
using ES.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Domain.Services
{
    public class StarshipService : IStarshipService
    {
        private readonly IStarshipRepository _starshipRepository;

        public StarshipService(IStarshipRepository starshipRepository)
        {
            _starshipRepository = starshipRepository;
        }

        public async Task<Starship> GetAsync(string Id, CancellationToken cancellationToken)
        {
            return await _starshipRepository.GetAsync(Id, cancellationToken);
        }

        private async Task SaveAsync(Starship starship, CancellationToken cancellationToken)
        {
            await _starshipRepository.SaveAsync(starship, cancellationToken);
        }

        public async Task<Starship> CreateStarshipAsync(string shipName, string currentLocation, string pilotName, CancellationToken cancellationToken)
        {
            var starship = Starship.CreateStartship(shipName, currentLocation, pilotName);
            await SaveAsync(starship, cancellationToken);
            return starship;
        }

        public async Task ChangePilot(string starshipId, string pilotName, CancellationToken cancellationToken)
        {
            var starship = await _starshipRepository.GetAsync(starshipId, cancellationToken);
            starship.SetPilot(pilotName);
            await SaveAsync(starship, cancellationToken);
        }

        public async Task Move(string starshipId, string planetName, int distance, CancellationToken cancellationToken)
        {
            var starship = await _starshipRepository.GetAsync(starshipId, cancellationToken);
            if (starship.ShipStatus == Types.Status.Landed)
                throw new Exception("Ship can't fly when is landed");

            starship.Move(planetName, distance);
            await SaveAsync(starship, cancellationToken);
        }

        public async Task TakeOff(string starshipId, CancellationToken cancellationToken)
        {
            var starship = await _starshipRepository.GetAsync(starshipId, cancellationToken);
            if (starship.ShipStatus == Types.Status.Flying)
                throw new Exception("Ship is already flying");

            starship.TakeOffShip();
            await SaveAsync(starship, cancellationToken);
        }

        public async Task Land(string starshipId, CancellationToken cancellationToken)
        {
            var starship = await _starshipRepository.GetAsync(starshipId, cancellationToken);
            if (starship.ShipStatus == Types.Status.Landed)
                throw new Exception("Ship is already landed");

            starship.LandShip();
            await SaveAsync(starship, cancellationToken);
        }

        public async Task<IEnumerable<Starship>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            return await _starshipRepository.GetAsync(pageIndex, pageSize, cancellationToken);
        }
    }
}
