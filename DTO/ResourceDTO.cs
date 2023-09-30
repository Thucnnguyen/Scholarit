using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.DTO
{

    public class ResourceDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public int ChapterId { get; set; }
        public int? ResourceParentId { get; set; }

    }
}
