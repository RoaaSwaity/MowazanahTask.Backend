using Data;

namespace Service.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public ApplicationDbContext context { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}
