namespace Scholarit.Entity
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public string? Type { get; set; }
        public int Difficult { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
        public int ChapterId { get; set; }
        public string Answer { get; set; }

        public virtual ICollection<QuizAttemptQuestion> QuizAttemptQuestion { get; set; }
        public virtual ICollection<Chapter> Chapter { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestion { get; set; }

    }
}
