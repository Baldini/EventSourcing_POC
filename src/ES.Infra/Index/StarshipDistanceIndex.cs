using ES.Domain.Events;
using ES.Domain.Results;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace ES.Infra.Index
{
    public class StarshipDistanceIndex : AbstractMultiMapIndexCreationTask<StarshipDistanceResult>
    {
        public StarshipDistanceIndex()
        {

            AddMap<FlyTo>(evnt =>
            from e in evnt
            where e.Name == nameof(FlyTo)
            select new
            {
                StarshipId = e.AggregateId,
                e.Distance,
                Places = 1,
                ShipName = string.Empty
            });

            AddMap<StarshipCreate>(evnts =>
            from e in evnts
            where e.Name == nameof(StarshipCreate)
            select new
            {
                StarshipId = e.AggregateId,
                e.ShipName,
                Places = 0,
                Distance = 0
            });

            Reduce = inputs => from input in inputs
                               group input by input.StarshipId
                               into g
                               select new StarshipDistanceResult
                               {
                                   StarshipId = g.Key,
                                   ShipName = g.Aggregate("", (a, b) => b.ShipName != string.Empty ? b.ShipName : a),
                                   Distance = g.Sum(c => c.Distance),
                                   Places = g.Sum(c => c.Places)
                               };
        }
    }
}
