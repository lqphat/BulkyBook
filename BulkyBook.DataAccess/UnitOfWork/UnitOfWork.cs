using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            Product = new ProductRepository(_context);
        }
        
        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
        
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}