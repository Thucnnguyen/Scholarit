namespace Scholarit.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CourseId { get; set; }
        public double? Rate { get; set; }
        public string? FeedBack { get; set; }
        public string CourseName { get; set; }
    }
}
