using Data.Dto_s;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly PhotoService _photoService;
        private readonly CyclesService _cyclesService;
        private readonly CoursesService _coursesService;
        private readonly EnrollmentsService _enrollmentsService;
        private readonly ReservationService _reservationService;
        private readonly StatusService _statusService;
        private readonly IWebHostEnvironment _environment;

        public UserController(IWebHostEnvironment environment, StatusService statusService, ReservationService reservationService, EnrollmentsService enrollmentsService, CyclesService cyclesService, CoursesService coursesService, UserService userService, PhotoService photoService)
        {
            _userService = userService;
            _photoService = photoService;
            _environment = environment;
            _coursesService = coursesService;
            _cyclesService = cyclesService;
            _enrollmentsService = enrollmentsService;
            _environment = environment;
            _reservationService = reservationService;
            _statusService = statusService;
        }

        public ActionResult Index()
        {
            return View(_userService.GetAll());
        }

        public ActionResult MyAccount()
        {
            return View(_userService.GetByEmail(User.Identity.Name));
        }

        public void EditUser(int id)
        {
            User user = _userService.GetById(id);
        }

        [HttpPost]
        public IActionResult MyUser(MyUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                _userService.UpdateUser(user);
                return RedirectToAction("MyUser");
            }
        }

        [HttpGet]
        public IActionResult MyUser()
        {
            MyUserDto? user = _userService.MyUser(User.Identity.Name);

            return View(user);
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            MyUserDto? user = _userService.MyUser(User.Identity.Name);
            var users = _userService.GetAll();
            var courses = _coursesService.GetAll();
            var cycles = _cyclesService.GetAll().OrderBy(x => x.StartDate);
            var statuses = _statusService.GetAll().ToList();
            var enrollments = _enrollmentsService.GetAll().Where(x => x.Course.Cycles?.OrderBy(y => y.EndDate).FirstOrDefault()?.EndDate > DateTime.Now).ToList();

            var currentDate = DateTime.Now.Date;
            var reservations = _reservationService.GetAll().Where(x => x.ReservationSlot?.Date + x.ReservationSlot?.StartTime >= currentDate && (x.Status?.Name == "Pending" || x.Status?.Name == "Confirmed")).ToList();

            ViewData["admin"] = user;
            ViewData["users"] = users;
            ViewData["allCourses"] = courses;
            ViewData["allCycles"] = cycles;
            ViewData["enrollments"] = enrollments;
            ViewData["reservations"] = reservations;
            ViewData["statuses"] = statuses;

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCurrentUserProfilePicture(IFormFile photo, int id)
        {
            MyUserDto? user = _userService.MyUser(User.Identity.Name);

            if (photo != null && photo.Length > 0)
            {
                var nazwaPliku = Path.GetFileName(photo.FileName);
                var ścieżkaZapisu = Path.Combine(_environment.WebRootPath, "images", nazwaPliku);

                using (var strumień = new FileStream(ścieżkaZapisu, FileMode.Create))
                {
                    await photo.CopyToAsync(strumień);
                }

                var file = nazwaPliku;
                _photoService.AddPhoto(file, id);
            }

            return RedirectToAction("MyUser", "User");
        }

    }
}
