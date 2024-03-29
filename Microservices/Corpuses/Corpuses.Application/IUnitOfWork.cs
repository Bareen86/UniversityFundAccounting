namespace Corpuses.Application
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
