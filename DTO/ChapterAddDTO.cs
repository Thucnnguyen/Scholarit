namespace Scholarit.DTO
{
    public class ChapterAddDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Content { get; set; }
        public int Duration { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }
		public string? Intro { get; set; }
		public string? Summary { get; set; }
	}
}
