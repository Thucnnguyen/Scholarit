using Microsoft.EntityFrameworkCore;
using Scholarit.Entity;

namespace Scholarit.Data
{
    public class ScholaritDbContext : DbContext
    {
        public ScholaritDbContext(DbContextOptions<ScholaritDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enroll>()
                .HasOne(e => e.Chapter)
                .WithMany(c => c.Enroll)
                .HasForeignKey(e => e.ChapterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enroll>()
                .HasOne(e => e.OrderDetail)
                .WithMany(od => od.Enroll)
                .HasForeignKey(e => e.OrderDetailId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Course)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(o => o.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuizAttemptQuestion>()
                .HasOne(qaq => qaq.Question)
                .WithMany(q => q.QuizAttemptQuestion)
                .HasForeignKey(qaq => qaq.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(qa => qa.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Enroll> Enroll { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Chapter> Chapter { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<QuizAttempt> QuizAttempt { get; set; }
        public virtual DbSet<QuizAttemptQuestion> QuizAttemptQuestion { get; set; }
        public virtual DbSet<QuizQuestion> QuizQuestion { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Users> User { get; set; }
    }
}
