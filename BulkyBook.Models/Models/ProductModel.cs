using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.Models
{
    public class ProductModel : BaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(1,10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        [Required]
        [ForeignKey("CoverTypeId")]
        public int CoverTypeId { get; set; }
        public CoverTypeModel CoverType { get; set; }

    }
}
    