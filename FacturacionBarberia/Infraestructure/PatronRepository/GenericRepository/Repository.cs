using FacturacionBarberia.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FacturacionBarberia.Infraestructure.PatronRepository.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _db;

        public Repository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .FindAsync(id);
        }

        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .AnyAsync(predicate);
        }

        public async Task<int> CountAsync(
            Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>()
                .AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>()
                .Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>()
                .Remove(entity);
        }

    }
}
