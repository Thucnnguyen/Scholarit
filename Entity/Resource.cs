namespace Scholarit.Entity
{
    public class Resource : BaseEntity
    {
        public string Type { get; set; }
        public string Url { get; set; }
        public int ChapterId { get; set; }
        
        public virtual Chapter Chapter { get; set; }

    }
}
