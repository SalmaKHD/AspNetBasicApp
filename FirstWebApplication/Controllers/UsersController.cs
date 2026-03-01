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
        public IActionResult Index()
        {
            List<string> users = _usersService.GetUsers();
            return View("Users", users);
        }
    }
}
