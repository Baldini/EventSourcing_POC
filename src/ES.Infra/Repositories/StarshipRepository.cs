using ES.Domain.Entities;
using ES.Domain.Persistence;
using ES.Domain.Persistence.EventStore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Infra.Repositories
{
    public class StarshipRepository : IStarshipRepository
    {
        private readonly IElasticClient _elasticClient;
        private const string DEFAULT_INDEX = "starship";
        private readonly IEventStore _eventStore;

        public StarshipRepository(IElasticClient elasticClient, IEventStore eventStore)
        {
            _elasticClient = elasticClient;
            _eventStore = eventStore;
        }

        public async Task<Starship> GetAsync(string Id, CancellationToken cancellationToken)
        {
            var ship = await _elasticClient.GetAsync<Starship>(Id, g => g.Index(DEFAULT_INDEX), cancellationToken);

            if (ship.IsValid)
                return ship.Source;

            throw new ArgumentNullException();
        }

        public async Task SaveAsync(Starship aggregate, CancellationToken cancellationToken)
        {
            await _elasticClient.IndexAsync(aggregate, g => g.Index(DEFAULT_INDEX).Id(aggregate.Id), cancellationToken);

            var evts = aggregate.GetUncommittedEvents();
            if (evts.Any())
            {
                await _eventStore.AppendEventAsync(evts, cancellationToken);
                aggregate.ClearUncommittedEvents();
            }
        }

        public async Task<IEnumerable<Starship>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var list = await _elasticClient.SearchAsync<Starship>(slc => slc.Index(DEFAULT_INDEX)
            .From((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Sort(s => s.Descending(f => f.Created))
            , cancellationToken
            );

            return list.Documents;
        }
    }
}
