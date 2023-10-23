namespace Scholarit.DTO
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int MaxScore { get; set; }
        public int NumberOfQuestion { get; set; }
        public int ChapterId { get; set; }
    }
}
