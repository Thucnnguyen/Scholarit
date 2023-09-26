using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Scholarit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public string Test()
        {
            return "sdfdsf";
        }
    }
}
