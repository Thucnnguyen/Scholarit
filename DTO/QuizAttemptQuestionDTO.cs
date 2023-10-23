namespace Scholarit.DTO
{
    public class QuizAttemptQuestionDTO
    {
        public int QuestionId { get; set; }
        public int QuizAttemptId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
