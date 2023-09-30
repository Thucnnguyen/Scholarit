using Scholarit.Entity;

namespace Scholarit.DTO
{
    public class OrderDTO
    {
        public double Price { get; set; }
        public int Status { get; set; }
        public int Note { get; set; }

        public virtual ICollection<OrderDetailDTO> OrderDetail { get; set; }
        public virtual UserDto User { get; set; }
    }
}
