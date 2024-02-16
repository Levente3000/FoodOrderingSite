using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Model
{
    public class FoodCategory
    {
        [Key]
        public string Name { get; set; } = null!;

        [Required]
        public string PictureName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
