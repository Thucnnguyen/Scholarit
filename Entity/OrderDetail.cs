namespace Scholarit.Entity
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int CourseId { get; set; }
        public double? Rate { get; set; }
        public string? FeedBack { get; set; }
        public string CourseName { get; set; }

        public virtual ICollection<Enroll> Enroll { get; set; }
        public virtual Course Course { get; set; }
        public virtual Order Order { get; set; }


    }
}
