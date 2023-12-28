using Data.Dto_s;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Security.Claims;

namespace Application.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetAllDto());
        }

        public PartialViewResult RegisterUser()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegisterUserDto dto)
        {
            if (ModelState.IsValid)
            {
                _userService.RegisterUserDto(dto);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = _userService.Login(dto);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is wrong.");
                return RedirectToAction("Index");
            }
        }

        public IActionResult LoggedIn()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
    }
}
