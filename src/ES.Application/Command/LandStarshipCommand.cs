using MediatR;

namespace ES.Application.Command
{
    public class LandStarshipCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
