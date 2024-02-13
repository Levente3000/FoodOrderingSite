using NodaTime;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderWebApi.Model
{
    public class OpeningHour
    {
        public int Id { get; set; }

        public Instant? Monday { get; set; }

        public Instant? Tuesday { get; set; }

        public Instant? Wednesday { get; set; }

        public Instant? Thursday { get; set; }

        public Instant? Friday { get; set; }

        public Instant? Saturday { get; set; }

        public Instant? Sunday { get; set; }
    }
}
