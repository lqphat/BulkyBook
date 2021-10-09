using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    class ProductRepository : GenericRepository<ProductModel>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<IEnumerable<ProductModel>> GetAll()
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.CoverType).ToListAsync();
        }
    }
}
