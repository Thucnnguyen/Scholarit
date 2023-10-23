namespace Scholarit.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
