namespace Scholarit.DTO
{
    public class ResourceUpdateDTO 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public int ChapterId { get; set; }
        public int? ResourceParentId { get; set; }

    }
}
