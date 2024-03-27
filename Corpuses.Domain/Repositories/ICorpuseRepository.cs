using Corpuses.Domain.Repositories.BasicRepositories;

namespace Corpuses.Domain.Repositories
{
    public interface ICorpuseRepository :
        IAddedRepository<Corpuse>,
        IRemovableRepository<Corpuse>

    {
        Task<Corpuse> GetByIdAsync(int id);
        Task<Corpuse> GetByNameAndAddressAsync( string name, string address );
    }
}
