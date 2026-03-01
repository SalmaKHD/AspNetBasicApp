using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace FirstWebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        // provided through DI
        public UsersController(IUsersService userService)
        {
            _usersService = userService;
        }

        [Route("users")]
        public IActionResult Index([FromServices] IUsersService usersServiceFromMethod)
        {
            List<string> users = usersServiceFromMethod.GetUsers();
            return View("Users", users);
        }
    }
}
