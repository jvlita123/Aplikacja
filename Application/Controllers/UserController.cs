using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;
        private RoleService _roleService;

        public UserController(UserService userService, RoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public ActionResult Index()
        {
            return View(_userService.GetAll());
        }
    }
}
