using System.ComponentModel.DataAnnotations;

namespace FoodOrderWebApi.Model
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string OrdererName { get; set; } = null!;

        [Required]
        public string OrdererAddress { get; set; } = null!;

        [Required]
        public string OrdererPhoneNumber { get; set; } = null!;

        [Required]
        public bool IsDone { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = null!;
    }
}
