namespace Audiences.Application
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
