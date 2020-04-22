using AutoMapper;
using ES.Application.Command;
using ES.Domain.DTO;
using ES.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Application.Handler
{
    public class WriteStarshipHandler :
        IRequestHandler<CreateStarshipCommand, StarshipDto>,
        IRequestHandler<MoveStarshipCommand, bool>,
        IRequestHandler<LandStarshipCommand, bool>,
        IRequestHandler<TakeOffCommand, bool>,
        IRequestHandler<ChangePilotCommand, bool>
    {
        private readonly IStarshipService _service;
        private readonly IMapper _mapper;

        public WriteStarshipHandler(IStarshipService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<StarshipDto> Handle(CreateStarshipCommand request, CancellationToken cancellationToken)
        {
            var ship = await _service.CreateStarshipAsync(request.ShipName, request.Location, request.PilotName, cancellationToken);

            return _mapper.Map<StarshipDto>(ship);
        }

        public async Task<bool> Handle(MoveStarshipCommand request, CancellationToken cancellationToken)
        {
            await _service.Move(request.Id, request.Location, request.Distance, cancellationToken);
            return true;
        }

        public async Task<bool> Handle(TakeOffCommand request, CancellationToken cancellationToken)
        {
            await _service.TakeOff(request.Id, cancellationToken);
            return true;
        }

        public async Task<bool> Handle(LandStarshipCommand request, CancellationToken cancellationToken)
        {
            await _service.Land(request.Id, cancellationToken);
            return true;
        }

        public async Task<bool> Handle(ChangePilotCommand request, CancellationToken cancellationToken)
        {
            await _service.ChangePilot(request.Id, request.PilotName, cancellationToken);
            return true;
        }
    }
}
