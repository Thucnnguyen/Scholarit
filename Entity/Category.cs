namespace Scholarit.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int TotalCourse { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
