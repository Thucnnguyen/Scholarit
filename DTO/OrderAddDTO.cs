namespace Scholarit.DTO
{
    public class OrderAddDTO
    {
        public double Price { get; set; }
        public string? Note { get; set; }

        public List<OrderDetailAddDTO> OrderDetails { get; set; }
    }
}
