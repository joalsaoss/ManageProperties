namespace ManagerProperties.Application.Contracts.Repositories
{
    public interface IRepository<T> where T : class 
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid id);
    }
}
