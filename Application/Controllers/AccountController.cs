using Data.Dto_s;
using Data.Entities;
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

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegisterUserDto dto)
        {
            if (ModelState.IsValid)
            {
                User newAccount = _userService.RegisterUserDto(dto);
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = _userService.Login(dto);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is wrong.");
                return RedirectToAction("Login");

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
