using FirstWebApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
                // ImageDownload is a reference to the method
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

        [Route("user")]
        [HttpPost]
        // if we recieve object as query data, individual fields for the object will be received as keys
        public IActionResult getUserWithName([FromQuery] string? name, [FromBody] Book? book) // name may be passed in any form, higher priority data will be picked
        {
            // act on model errors
            if(!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage);
                string errors = string.Join("\n", errorMessages);

                return BadRequest(errors);
            }

            return Content($"name of this user is {name} with book {book}");
        }
    }
}
