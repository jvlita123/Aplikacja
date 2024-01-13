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
        private readonly UserService _userService;
        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            else
            {
                _userService.RegisterUserDto(dto);
                return RedirectToAction("Home", "Home");
            }
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPage(LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claimsIdentity = _userService.Login(dto);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Home", "Home");
            }
            else
            {
                return View(dto);
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
            return RedirectToAction("Home", "Home");
        }
    }
}
