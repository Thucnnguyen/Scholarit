namespace Scholarit.DTO
{
    public class OrderAndDetailDTO
    {
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public int UserId { get; set; }
        public DateTime? DateCreated { get; set; }
        
        public List<OrderDetailDTO> Items { get; set; } = new List<OrderDetailDTO>();
    }
}
