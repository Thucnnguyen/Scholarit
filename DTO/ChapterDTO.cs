namespace Scholarit.DTO
{
    
    public class ChapterDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Content { get; set; }
        public int Duration { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }
    }
}
