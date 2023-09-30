namespace Scholarit.Entity
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; } = 0;
        public string? Author { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int NumberOfChapter { get; set; } = 0;
        public string? CoverImgUrl { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Chapter> Chapter { get; set; }
        public virtual ICollection<Enroll> Enroll { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
