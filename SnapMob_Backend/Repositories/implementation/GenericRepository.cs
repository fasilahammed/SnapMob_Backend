using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Data;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace SnapMob_Backend.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet.Where(e => !e.IsDeleted);

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
