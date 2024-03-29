namespace Audiences.Domain.Repositories.BasicRepositories
{
    public interface IRemovableRepository<TEntiry> where TEntiry : class
    {
        void Delete( TEntiry entiry );
    }
}
