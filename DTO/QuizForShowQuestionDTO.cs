namespace Scholarit.DTO
{
    public class QuizForShowQuestionDTO
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int MaxScore { get; set; }
        public int NumberOfQuestion { get; set; }
        public int ChapterId { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}
