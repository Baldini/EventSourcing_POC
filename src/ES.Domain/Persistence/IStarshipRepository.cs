using ES.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ES.Domain.Persistence
{
    public interface IStarshipRepository
    {
        Task<Starship> GetAsync(string Id, CancellationToken cancellationToken);
        Task SaveAsync(Starship aggregate, CancellationToken cancellationToken);
        Task<IEnumerable<Starship>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
