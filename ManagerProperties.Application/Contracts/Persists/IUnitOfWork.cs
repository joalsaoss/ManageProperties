namespace ManagerProperties.Application.Contracts.Persists
{
    public interface IUnitOfWork
    {
        Task Persist();
        Task RollBack();
    }
}
