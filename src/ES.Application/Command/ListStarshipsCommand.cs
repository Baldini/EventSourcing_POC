using ES.Domain.DTO;
using MediatR;

namespace ES.Application.Command
{
    public class ListStarshipsCommand : IRequest<StarshipListDto>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
