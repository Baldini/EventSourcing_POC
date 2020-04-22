using ES.Domain.Core;
using ES.Domain.Persistence.EventStore;
using ES.Domain.Results;
using ES.Infra.Index;
using Raven.Client.Documents;
using Raven.Client.Documents.BulkInsert;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Infra.Repositories
{
    public class EventStore : IEventStore
    {
        private readonly IDocumentStore _store;
        private readonly IAsyncDocumentSession _session;

        public EventStore(IDocumentStore store, IAsyncDocumentSession session)
        {
            _store = store;
            _session = session;
        }

        public async Task AppendEventAsync(IEnumerable<DomainEventBase> events, CancellationToken cancellationToken)
        {
            using (BulkInsertOperation bulkInsert = _store.BulkInsert())
            {
                foreach (var item in events)
                {
                    await bulkInsert.StoreAsync(item);
                }
            }
        }

        public async Task<StarshipDistanceResult> GetStarshipDistanceAsync(string aggregateId, CancellationToken cancellationToken)
        {
            var evnts = await _session.Query<StarshipDistanceResult, StarshipDistanceIndex>().Where(t => t.StarshipId == aggregateId).ToListAsync(cancellationToken);

            return evnts.FirstOrDefault();
        }

        public async Task<IEnumerable<DomainEventBase>> ReadEventsAsync(string aggregateId, CancellationToken cancellationToken)
        {
            var evnts = await _session.Query<DomainEventBase>().Where(t => t.AggregateId == aggregateId.ToString()).ToListAsync(cancellationToken);

            return evnts;
        }

        public async Task<IEnumerable<PilotResult>> GetPilotsHistoryAsync(string aggregateId, CancellationToken cancellationToken)
        {
            var evnts = await _session.Query<PilotResult, PilotIndex>().Where(t => t.StarshipId == aggregateId).ToListAsync();

            return evnts;
        }
    }
}
