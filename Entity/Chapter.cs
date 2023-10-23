namespace Scholarit.Entity
{
    public class Chapter : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Content { get; set; }
        public string? Intro { get; set; }
        public string? Summary {  get; set; } 
        public int Duration { get; set; }
        public int Order { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Quiz> Quiz { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Resource> Resource { get; set; }
        public virtual ICollection<Enroll> Enroll { get; set; }
    }
}
