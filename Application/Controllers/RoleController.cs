using Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class RoleController : Controller
    {
        private RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            List<Role> roles = _roleService.GetAll().ToList();

            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role? role)
        {
                _roleService.Add(role);
                return RedirectToAction(nameof(Index));
        }
    }
}
