using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Entities;
using Entities.IdentityEntities;
using FirstWebApplication.NewFolder4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using ServiceContracts.DTO;
using ServiceContracts.Models;

namespace FirstWebApplication.Areas.Admin.Controllers
{
    [Controller]
    [Area("Admin")]
    [Authorize(Roles ="Admin")] // this page will be available for admins only
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
