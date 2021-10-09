using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : GenericRepository<CategoryModel>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
}