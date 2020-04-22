using ES.Domain.Types;

namespace ES.Domain.Events
{
    public class TakeOff : BaseStatus
    {
        public TakeOff() : base(Status.Flying) { }

        public override string Name => nameof(TakeOff);

        public static TakeOff Create() => new TakeOff();
    }
}
