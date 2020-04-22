using ES.Domain.Core;
using ES.Domain.Types;

namespace ES.Domain.Events
{
    public abstract class BaseStatus : DomainEventBase
    {
        public Status ShipStatus { get; private set; }

        public BaseStatus(Status shipStatus)
        {
            ShipStatus = shipStatus;
        }
    }
}
