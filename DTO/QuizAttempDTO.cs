using Scholarit.Entity;

namespace Scholarit.DTO
{
    public class QuizAttempDTO
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int Attempt { get; set; }
        public int Score { get; set; }
        public DateTime LastAttempt { get; set; } = DateTime.Now;

        public  List<QuizAttemptQuestionDTO> QuizAttempQuestions { get; set; } = new List<QuizAttemptQuestionDTO>();
    }
}
