using ES.Domain.Core;
using System.Collections.Generic;

namespace ES.Domain.Persistence
{
    public interface IEventSourcingAggregate
    {
        long Version { get; }
        IEnumerable<DomainEventBase> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
