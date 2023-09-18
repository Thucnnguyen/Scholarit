namespace Scholarit.Entity
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int MaxScore { get; set; }
        public int NumberOfQuestion { get; set; }
        public int ChapterId { get; set; }
        
        public virtual Chapter Chapter { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
