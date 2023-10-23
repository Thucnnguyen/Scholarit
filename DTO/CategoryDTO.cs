using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.DTO
{

    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCourse { get; set; }

	}
}
