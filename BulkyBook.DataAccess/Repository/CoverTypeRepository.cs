using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : GenericRepository<CoverTypeModel>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public CoverTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
