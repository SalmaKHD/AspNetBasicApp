using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplication.Controllers
{
    [Route("api/[controller]")]
    [Route("api/country")]
    [ApiController]
    public class CountryRestController : ControllerBase
    {
        [HttpGet]
        public string Method()
        {
            return "Hello from Web API";
        }
    }
}
