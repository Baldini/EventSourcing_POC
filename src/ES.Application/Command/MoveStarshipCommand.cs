using MediatR;
using Newtonsoft.Json;

namespace ES.Application.Command
{
    public class MoveStarshipCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Location { get; set; }
        public int Distance { get; set; }
    }
}
