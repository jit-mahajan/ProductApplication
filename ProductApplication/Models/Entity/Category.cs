using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductApplication.Models.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
