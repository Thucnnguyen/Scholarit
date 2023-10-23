using System.ComponentModel.DataAnnotations;

namespace Scholarit.Entity
{
    public class Enroll : BaseEntity
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int? ChapterId { get; set; }
        public int OrderDetailId { get; set; }
        public string? Note { get; set; }
        public bool IsFinished { get; set; } = false;
        public virtual Course Course { get; set; }
        public virtual Chapter Chapter { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Users User { get; set; }
    }
}
