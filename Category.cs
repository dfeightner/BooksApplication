using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApplication.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "The name for the category MUST be provided")]
        public string Name { get; set; }

        [DisplayName("Category Description"), Required(ErrorMessage = "The Category Description MUST be provided")]
        public string Description { get; set; }


    }
}
