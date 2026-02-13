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
        public IActionResult ImageDownloadWithId() // represents an action
        {
            if (Convert.ToInt32(ControllerContext.HttpContext.Request.RouteValues["id"]) == 1)
            {
                // a shortcut method in Controller
                return RedirectToAction(nameof(ImageDownload), new { }); // second argument is for forwarding values from request

                // 302 status code: permenant redirection
                //return RedirectToActionPermanent(nameof(ImageDownload), new { });

                // redirect to a url that is local to app
                //return LocalRedirect("/get-image"); // 302, found
                //return LocalRedirectPermanent("/get-image"); // 301, moved permanently

                // redirect to a url outside application
                //return Redirect("https://google.com"); // 302
                //return RedirectPermanent("https://google.com"); //301
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
