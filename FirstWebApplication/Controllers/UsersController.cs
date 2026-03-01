using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace FirstWebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IServiceScopeFactory _serviceFactory;

        // provided through DI
        public UsersController(IUsersService userService, IServiceScopeFactory serviceFactory)
        {
            _usersService = userService;
            _serviceFactory = serviceFactory;
        }

        [Route("users")]
        public IActionResult Index([FromServices] IUsersService usersServiceFromMethod)
        {
            // create a child scope inside request -> will be destructed after using block
            using (var scope = _serviceFactory.CreateScope())
            {
                IUsersService usersServiceScoped = scope.ServiceProvider.GetRequiredService<IUsersService>();
                // service disposed here
            }
            List<string> users = usersServiceFromMethod.GetUsers();
            return View("Users", users);
            /**
             * output
             * OnDispose of Users Service being called
             * OnDispose of Users Service being called
             */
        }
    }
}
