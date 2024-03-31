using Audiences.Domain.Repositories.BasicRepositories;

namespace Audiences.Domain.Repositories
{
    public interface IAudienceRepository :
        IAddedRepository<Audience>,
        IRemovableRepository<Audience>,
        ISearchRepository<Audience>
    {
        Task<IReadOnlyList<Audience>> GetAudiencesByCorpuseIdAsync(int id);
        Task<Audience> GetAudienceByIdAsync( int id );
        Task DeleteAudiencesByCorpuseIdAsync( int id );
    }
}
