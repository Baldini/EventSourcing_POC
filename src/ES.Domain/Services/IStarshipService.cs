using ES.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Domain.Services
{
    public interface IStarshipService
    {
        Task ChangePilot(string starshipId, string pilotName, CancellationToken cancellationToken);
        Task<Starship> CreateStarshipAsync(string shipName, string currentLocation, string pilotName, CancellationToken cancellationToken);
        Task<Starship> GetAsync(string Id, CancellationToken cancellationToken);
        Task Land(string starshipId, CancellationToken cancellationToken);
        Task Move(string starshipId, string planetName, int distance, CancellationToken cancellationToken);
        Task TakeOff(string starshipId, CancellationToken cancellationToken);
        Task<IEnumerable<Starship>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}