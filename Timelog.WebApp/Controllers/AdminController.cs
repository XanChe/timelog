using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TimelogWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> manager)
        {
            _userManager = manager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
