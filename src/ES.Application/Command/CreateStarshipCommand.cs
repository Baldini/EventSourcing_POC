using ES.Domain.DTO;
using MediatR;

namespace ES.Application.Command
{
    public class CreateStarshipCommand : IRequest<StarshipDto>
    {
        public string ShipName { get; set; }
        public string Location { get; set; }
        public string PilotName { get; set; }
    }
}
