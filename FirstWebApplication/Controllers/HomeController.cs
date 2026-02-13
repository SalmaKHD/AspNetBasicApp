using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplication.NewFolder4
{
    public class HomeController : Controller
    {
        // add attribute routing
        [Route("home")]
        public string Index()
        {
            return "Hello from home";
        }
    }
}
