namespace Scholarit.DTO
{
    public class CourseAddDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        //public int Duration { get; set; }
        public string? Author { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        // int NumberOfChapter { get; set; }
        public int CategoryId { get; set; }
    }
}
