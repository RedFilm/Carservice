using Carservice.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carservice.Controllers
{
    public class ManagerPanelController : Controller
    {
        private SignInManager<AppUser> _signInMnr;
        private UserManager<AppUser> _userMnr;
        private RoleManager<IdentityRole> _roleMnr;

        public ManagerPanelController(SignInManager<AppUser> signInMnr,
                              UserManager<AppUser> userMnr,
                              RoleManager<IdentityRole> roleMnr)
        {
            _signInMnr = signInMnr;
            _userMnr = userMnr;
            _roleMnr = roleMnr;
        }

        [HttpGet]
        public IActionResult Index() => View();
    }
}
