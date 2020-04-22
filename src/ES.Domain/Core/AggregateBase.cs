using ES.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ES.Domain.Core
{
    public abstract class AggregateBase : IEventSourcingAggregate
    {
        private readonly ICollection<DomainEventBase> _uncommittedEvents = new LinkedList<DomainEventBase>();

        public AggregateBase() => Version = 0;

        public string Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public long Version { get; protected set; }

        public void ApplyEvent(DomainEventBase @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
            Version = @event.AggregateVersion;
        }


        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

        public IEnumerable<DomainEventBase> GetUncommittedEvents() => _uncommittedEvents.AsEnumerable();

        protected void RaiseEvent<TEvent>(TEvent @event) where TEvent : DomainEventBase
        {
            UpVersion();
            @event.SetAggregateVersion(Version);
            @event.SetAggregateId(Id);

            _uncommittedEvents.Add(@event);
            ApplyEvent(@event);
        }

        protected void UpVersion()
        {
            Version += 1;
        }
    }
}
