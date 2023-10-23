namespace Scholarit.Entity
{
    public class QuizAttemptQuestion : BaseEntity
    {
        public int QuestionId { get; set; }
        public int QuizAttemptId { get; set; }
        public string Answer { get; set; }
        public int UserId { get; set; }
        public bool IsCorrect { get; set; }

        public virtual Question Question { get; set; }
        public virtual Users User { get; set; }
        public virtual QuizAttempt QuizAttempt { get; set; }
    }
}
