namespace RedFalcon.Application.Interfaces.Data
{
    public interface IBaseRepository<T> where T : class
    {
        // Transaction
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T?> GetByIdAsync(int id);
    }
}
