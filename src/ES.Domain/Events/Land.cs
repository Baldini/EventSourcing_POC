using ES.Domain.Types;

namespace ES.Domain.Events
{
    public class Land : BaseStatus
    {
        public Land() : base(Status.Landed) { }

        public override string Name => nameof(Land);

        public static Land Create() => new Land();
    }
}
