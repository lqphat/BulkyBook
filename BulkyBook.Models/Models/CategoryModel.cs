using System;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models.Models
{
    public class CategoryModel : BaseModel
    {
        [Required]
        [Display(Name = "Category Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        
    }
}