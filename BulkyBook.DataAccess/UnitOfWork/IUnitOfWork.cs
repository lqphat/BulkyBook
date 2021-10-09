using System;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        Task SaveChanges();
    }
}