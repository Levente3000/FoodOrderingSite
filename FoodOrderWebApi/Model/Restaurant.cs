using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Model
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string LogoName { get; set; } = null!;

        [Required]
        public int OpeningHourId { get; set; }
        [ForeignKey("OpeningHourId")]
        public virtual OpeningHour OpeningHours { get; set; } = null!;

        [Required]
        public int ClosingHourId { get; set; }
        [ForeignKey("ClosingHourId")]
        public virtual OpeningHour ClosingHours { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = null!; 
    }
}
