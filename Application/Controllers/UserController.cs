using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_roleService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                _userService.Add(user);
                ViewData["RoleId"] = new SelectList(_roleService.GetAll(), "Id", "Name", user.RoleId);
                return View(user);
            }
            catch
            {
                return View();
            }
        }
    }
}
