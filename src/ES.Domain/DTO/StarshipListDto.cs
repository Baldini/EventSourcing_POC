using System.Collections.Generic;

namespace ES.Domain.DTO
{
    public class StarshipListDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<StarshipDto> Ships { get; set; }
    }
}
