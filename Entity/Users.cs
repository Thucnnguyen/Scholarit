namespace Scholarit.Entity
{
    public class Users : BaseEntity
    {
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Hobby { get; set; }
        public DateTime LastLogin { get; set; }
        public int? LearnHourPerDay { get; set; }
        public string? Strength { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AvatarUrl { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Enroll> Enroll { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
