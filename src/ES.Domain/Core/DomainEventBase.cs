using System;

namespace ES.Domain.Core
{
    public abstract class DomainEventBase
    {
        public DomainEventBase()
            => Created = DateTime.UtcNow;

        public string AggregateId { get; protected set; }
        public long AggregateVersion { get; protected set; }
        public DateTime Created { get; protected set; }

        public abstract string Name { get; }

        public void SetAggregateId(string id)
            => AggregateId = id;

        public void SetAggregateVersion(long aggregateVersion)
            => AggregateVersion = aggregateVersion;

        public void SetCreated(DateTime created)
            => Created = created;
    }
}
