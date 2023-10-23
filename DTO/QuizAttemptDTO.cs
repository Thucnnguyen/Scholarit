namespace Scholarit.DTO
{
    public class QuizAttemptDTO 
    {
        public int QuizId { get; set; }
        public List<QuestionAnwserDTO> Anwsers { get; set; }
    }
}
