using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ActionResult MyAccount()
        {
            return View(_userService.GetByEmail(User.Identity.Name));
        }

        public IActionResult EditUser(int id)
        {
            User user = _userService.GetById(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            _userService.UpdateUser(user);
            return RedirectToAction("MyAccount");
        }
    }
}
