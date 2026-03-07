using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

/**
 * base controller for country rest controller
 */
namespace FirstWebApplication.Controllers
{
    //[Route("api/[controller]")]
    [EnableCors("4100Client")] // add CORS setting
    [Route("api/country")]
    [ApiController] // allows options like automatic json request body parsing + 400 bad request when state not valid
        public class BaseCountryRestController : ControllerBase;
}
