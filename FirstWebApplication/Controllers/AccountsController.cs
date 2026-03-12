using Entities;
using Entities.IdentityEntities;
using FirstWebApplication.NewFolder4;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;

namespace FirstWebApplication.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = register.Email,
                    PhoneNumber = register.Phone,
                    UserName = register.Email,
                    PersonName = register.PersonName
                };
                IdentityResult result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    // creates a cookie and send as part of the response
                    await _signInManager.SignInAsync(user, isPersistent: true); // cookie persistant or not upon session
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                    return View(register);
                }
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(register);
            }
        }
    }
}