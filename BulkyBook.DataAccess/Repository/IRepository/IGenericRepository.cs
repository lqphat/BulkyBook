using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<T> GetById(int? id);
        Task<IEnumerable<T>> GetAll();
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task Delete(int? id);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}