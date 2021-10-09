using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where  T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<T> GetById(int? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task Delete(int? id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where<T>(expression);
        }
    }
}