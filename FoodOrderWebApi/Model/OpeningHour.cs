using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Model
{
    public class OpeningHour
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;

        public DateTime? Monday { get; set; }

        public DateTime? Tuesday { get; set; }

        public DateTime? Wednesday { get; set; }

        public DateTime? Thursday { get; set; }

        public DateTime? Friday { get; set; }

        public DateTime? Saturday { get; set; }

        public DateTime? Sunday { get; set; }
    }
}
