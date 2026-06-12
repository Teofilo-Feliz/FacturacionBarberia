using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate);

    Task<T?> GetByIdAsync(int id);

    Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate);

    Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}