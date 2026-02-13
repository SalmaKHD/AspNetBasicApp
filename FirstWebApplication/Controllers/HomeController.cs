using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplication.NewFolder4
{
    [Controller]
    public class HomeController : Controller
    {
        // add attribute routing
        [Route("/")]
        [Route("home")]
        public ContentResult Index()
        {
            // there are shortcut methods for all types
            return Content("Hello from home", "text/plain"); // a method provided by the Controller method
        }

        [Route("about-user/{id:int}")]
        public string AboutUser()
        {
            return $"this is user with id {ControllerContext.HttpContext.Request.RouteValues["id"]}";
        }

        [Route("get-image")]
        public VirtualFileResult ImageDownload()
        {
            return File("/sample.jpg", "image/jpeg"); // File is a shortcut for new VirtualFileResult()
        }

        // alternative return type: can return more than one type
        [Route("get-image/{id:int}")]
        public IActionResult ImageDownloadWithId()
        {
            if (Convert.ToInt32(ControllerContext.HttpContext.Request.RouteValues["id"]) == 1)
            {
                return File("/sample.jpg", "image/jpeg");
            } else
            {
                return BadRequest();
            }
        }
    }
}
