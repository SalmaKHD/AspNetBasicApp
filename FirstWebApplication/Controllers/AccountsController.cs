using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace FirstWebApplication.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountsController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDTO register)
        {
            //if (ModelState.IsValid)
            //{

            //}
            //else
            //{
            //    ValidatorHel
            //}
            return View();
        }
    }
}
