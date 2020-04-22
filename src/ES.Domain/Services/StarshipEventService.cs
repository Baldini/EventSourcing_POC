using ES.Domain.Core;
using ES.Domain.Persistence.EventStore;
using ES.Domain.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Domain.Services
{
    public class StarshipEventService : IStarshipEventService
    {
        private readonly IEventStore _eventStore;

        public StarshipEventService(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<StarshipDistanceResult> GetDistanceTravelled(string starshipId, CancellationToken cancellationToken)
        {
            return await _eventStore.GetStarshipDistanceAsync(starshipId, cancellationToken);
        }

        public async Task<IEnumerable<DomainEventBase>> GetEventsAsync(string starshipId, CancellationToken cancellationToken)
        {
            return await _eventStore.ReadEventsAsync(starshipId, cancellationToken);
        }

        public async Task<IEnumerable<PilotResult>> GetPilotsHistoryAsync(string starshipId, CancellationToken cancellationToken)
        {
            return await _eventStore.GetPilotsHistoryAsync(starshipId, cancellationToken);
        }
    }
}
