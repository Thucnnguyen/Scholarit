namespace Scholarit.DTO
{
    public class QuizAttemptAddOrUpdateDTO
    {
        //public int Id { get; set; } = -1;
        public int QuizId { get; set; }
        public List<QuestionAnwserDTO> Questions { get; set; }
    }
}
