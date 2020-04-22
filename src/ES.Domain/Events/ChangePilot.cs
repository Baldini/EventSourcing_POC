using ES.Domain.Core;

namespace ES.Domain.Events
{
    public class ChangePilot : DomainEventBase
    {
        public ChangePilot(string pilotName)
        {
            PilotName = pilotName;
        }
        public override string Name => nameof(ChangePilot);

        public string PilotName { get; private set; }

        public static ChangePilot Create(string pilotName) => new ChangePilot(pilotName);
    }
}
