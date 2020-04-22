using ES.Domain.Events;
using ES.Domain.Results;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace ES.Infra.Index
{
    public class PilotIndex : AbstractMultiMapIndexCreationTask<PilotResult>
    {
        public PilotIndex()
        {

            AddMap<ChangePilot>(evnts =>
            from e in evnts
            where e.Name == nameof(ChangePilot)
            select new
            {
                StarshipId = e.AggregateId,
                e.PilotName,
                Date = e.Created
            });

            AddMap<StarshipCreate>(evnts =>
            from e in evnts
            where e.Name == nameof(StarshipCreate)
            select new
            {
                StarshipId = e.AggregateId,
                e.PilotName,
                Date = e.Created
            });

            Reduce = inputs =>
            from input in inputs
            select new PilotResult
            {
                StarshipId = input.StarshipId,
                PilotName = input.PilotName,
                Date = input.Date
            };
        }
    }
}
