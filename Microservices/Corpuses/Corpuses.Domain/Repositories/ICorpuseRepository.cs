using Corpuses.Domain.Repositories.BasicRepositories;

namespace Corpuses.Domain.Repositories
{
    public interface ICorpuseRepository :
        IAddedRepository<Corpuse>,
        IRemovableRepository<Corpuse>,
        ISearchRepository<Corpuse>
    {
        Task<Corpuse> GetByIdAsync(int id);
        Task<Corpuse> GetByNameAndAddressAsync( string name, string address );
        Task<IReadOnlyList<Corpuse>> GetCorpusesAsync();
    }
}
