namespace Scholarit.Entity
{
    public class Order : BaseEntity
    {
        public double Price { get; set; }
        public int Status { get; set; }
        public int Note { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual Users User { get; set; }

    }
}
