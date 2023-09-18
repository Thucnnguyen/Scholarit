namespace Scholarit.Entity
{
    public class QuizAttempt : BaseEntity
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int Attempt { get; set; }
        public int Score { get; set; }
        public DateTime LastAttempt { get; set; }


        public virtual Quiz Quiz { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<QuizAttemptQuestion> QuizAttempQuestions { get; set; }
    }
}
