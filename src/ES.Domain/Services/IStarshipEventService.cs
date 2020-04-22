using ES.Domain.Core;
using ES.Domain.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Domain.Services
{
    public interface IStarshipEventService
    {
        Task<IEnumerable<DomainEventBase>> GetEventsAsync(string starshipId, CancellationToken cancellationToken);

        Task<IEnumerable<PilotResult>> GetPilotsHistoryAsync(string starshipId, CancellationToken cancellationToken);

        Task<StarshipDistanceResult> GetDistanceTravelled(string starshipId, CancellationToken cancellationToken);
    }
}