using System;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

    }
}