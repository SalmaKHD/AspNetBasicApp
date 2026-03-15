using Entities;
using Entities.IdentityEntities;
using FirstWebApplication.NewFolder4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Models;

namespace FirstWebApplication.Controllers
{
    [Route("[controller]/[action]")]
    // accessable without login
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService,
            ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[Authorize("NotAuthorized")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                return View(register);
            }

            var user = new ApplicationUser
            {
                Email = register.Email,
                PhoneNumber = register.Phone,
                UserName = register.Email,
                PersonName = register.PersonName
            };

            // create jwt token
            var token = _jwtService.CreateJwtToken(user);
            _logger.LogDebug($"jwt token is: {token}");
            user.Refresh = token.RefreshToken;
            user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;

            var createResult = await _userManager.CreateAsync(user, register.Password);

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                    ModelState.AddModelError("Register", error.Description);
                return View(register);
            }

            // Determine selected role
            var selectedRole = register.UserType == ServiceContracts.Models.UserTypeOptions.Admin
                ? UserTypeOptions.Admin.ToString()
                : UserTypeOptions.User.ToString();

            // Ensure role exists
            var roleExists = await _roleManager.RoleExistsAsync(selectedRole);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = selectedRole });
            }

            // Add user to the role
            await _userManager.AddToRoleAsync(user, selectedRole);

            // Sign them in
            await _signInManager.SignInAsync(user, isPersistent: true);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }



        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                // creates and gives back cookie automatically
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: true, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    var user = await _userManager.FindByEmailAsync(login.UserName);
                    if (user != null)
                    {
                        // create jwt token
                        var token = _jwtService.CreateJwtToken(user);
                        user.Refresh = token.RefreshToken;
                        user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
                        _logger.LogDebug($"jwt token is: {token}");

                        await _userManager.UpdateAsync(user);

                        if (await _userManager.IsInRoleAsync(user!, UserTypeOptions.Admin.ToString()))
                        {
                            return RedirectToAction("Index", "Home", new
                            {
                                area = "Admin" // redirect to admin area
                            });
                        }
                    }

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        // redirection happens only to the same domain
                        return LocalRedirect(returnUrl);
                    }

                    ModelState.AddModelError("Login", "User does not exist");
                    return View(login);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(login);
            }
        }

        public async Task<IActionResult> Logout()
        {
            // deletes user cookie
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}