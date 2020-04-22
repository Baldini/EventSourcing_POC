using ES.Application.Command;
using ES.Domain.DTO;
using ES.Domain.Results;
using ES.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarshipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStarshipEventService _event;
        private readonly IStarshipService _service;

        public StarshipController(IMediator mediator, IStarshipEventService @event, IStarshipService service)
        {
            _mediator = mediator;
            _event = @event;
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<StarshipListDto>> ListStarships(CancellationToken cancellationToken, int pageIndex = 1, int pageSize = 10)
        {
            var res = await _mediator.Send(new ListStarshipsCommand { PageIndex = pageIndex, PageSize = pageSize }, cancellationToken);
            return Ok(res);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<StarshipDto>> GetStartship(string id, CancellationToken cancellationToken)
        {
            var res = await _service.GetAsync(id, cancellationToken);

            return Ok(res);
        }

        [HttpGet]
        [Route("{id}/locations")]
        public async Task<ActionResult<StarshipDistanceResult>> GetCountPlacesByStarship(string id, CancellationToken cancellationToken)
        {
            var places = await _event.GetDistanceTravelled(id, cancellationToken);
            return Ok(places);
        }

        [HttpGet]
        [Route("{id}/pilots")]
        public async Task<ActionResult<IEnumerable<PilotDto>>> GetStartshipPilots(string id, CancellationToken cancellationToken)
        {
            var res = await _event.GetPilotsHistoryAsync(id, cancellationToken);

            return Ok(res);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> CreateStarship(CreateStarshipCommand request, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(request, cancellationToken);

            return CreatedAtAction(nameof(GetStartship), new { id = res.Id }, res);
        }

        [HttpPut]
        [Route("{id}/move")]
        public async Task<ActionResult> MoveStartship(string id, MoveStarshipCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var res = await _mediator.Send(request, cancellationToken);

            if (res)
                return Accepted();
            else
                return BadRequest();

        }

        [HttpPut]
        [Route("{id}/pilot")]
        public async Task<ActionResult> ChangePilot(string id, ChangePilotCommand request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var res = await _mediator.Send(request, cancellationToken);

            if (res)
                return Accepted();
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("{id}/takeoff")]
        public async Task<ActionResult> TakeOff(string id, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new TakeOffCommand { Id = id }, cancellationToken);

            if (res)
                return Accepted();
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("{id}/land")]
        public async Task<ActionResult> Land(string id, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(new LandStarshipCommand { Id = id }, cancellationToken);

            if (res)
                return Accepted();
            else
                return BadRequest();
        }
    }
}