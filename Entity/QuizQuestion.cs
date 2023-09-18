namespace Scholarit.Entity
{
    public class QuizQuestion : BaseEntity
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public int Order { get; set; }

        public virtual Question Question { get; set; } 
        public virtual Quiz Quiz { get; set; }
    
    }
}
