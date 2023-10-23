namespace Scholarit.DTO
{
    public class EnrollDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int? ChapterId { get; set; }
        public int OrderDetailId { get; set; }
        public string Note { get; set; }
        public bool IsFinished { get; set; }
    }
}
