using MediatR;
using Newtonsoft.Json;

namespace ES.Application.Command
{
    public class ChangePilotCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string PilotName { get; set; }
    }
}
