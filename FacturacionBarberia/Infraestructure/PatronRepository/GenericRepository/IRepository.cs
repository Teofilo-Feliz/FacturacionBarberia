using System.Linq.Expressions;

namespace FacturacionBarberia.Infraestructure.PatronRepository.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
