using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductModel Product { get; set; }
        public IEnumerable<CategoryModel> CategoryList { get; set; }
        public IEnumerable<CoverTypeModel> CoverTypeList { get; set; }
    }
}
