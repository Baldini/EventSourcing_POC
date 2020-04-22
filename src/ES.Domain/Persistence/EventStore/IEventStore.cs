using ES.Domain.Core;
using ES.Domain.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Domain.Persistence.EventStore
{
    public interface IEventStore
    {
        Task AppendEventAsync(IEnumerable<DomainEventBase> events, CancellationToken cancellationToken);
        Task<IEnumerable<DomainEventBase>> ReadEventsAsync(string aggregateId, CancellationToken cancellationToken);

        Task<StarshipDistanceResult> GetStarshipDistanceAsync(string aggregateId, CancellationToken cancellationToken);
        Task<IEnumerable<PilotResult>> GetPilotsHistoryAsync(string aggregateId, CancellationToken cancellationToken);
        //Task<IEnumerable<StarshipPlacesResult>> GetPlaces(CancellationToken cancellationToken);
    }
}
