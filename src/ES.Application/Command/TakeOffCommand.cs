using MediatR;

namespace ES.Application.Command
{
    public class TakeOffCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
