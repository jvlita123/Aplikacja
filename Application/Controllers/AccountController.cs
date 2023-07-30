using Data.Dto_s;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;

namespace Application.Controllers
{
    //[Route("api/account")]
    //[ApiController]
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
        //[HttpGet("register")]
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
    }
}
