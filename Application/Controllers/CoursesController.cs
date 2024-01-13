using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services;


namespace Application.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserService _userService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly IWebHostEnvironment _environment;


        public CoursesController(IWebHostEnvironment environment, CoursesService coursesService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _userService = userService;
            _enrollmentService = enrollmentsService;
            _coursesService = coursesService;
            _environment = environment;
        }

        [Route("AllCourses")]
        public IActionResult Index()
        {
            List<Course> courses = _coursesService.GetAll();
            return View(courses);
        }

        public IActionResult GetCourse(int id)
        {
            Course course = _coursesService.GetById(id);
            var cycles = course.Cycles.OrderBy(x => x.StartDate);
            var enrolledUsers = _enrollmentService.GetEnrolledUserByCourse(id);

            ViewData["cycles"] = cycles;
            ViewData["enrolledUsers"] = enrolledUsers;

            return View(course);
        }

        [HttpGet]
        public PartialViewResult NewCourse()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> NewCourse(Course course, IFormFile uploadFile)
        {
            if (uploadFile != null && uploadFile.Length > 0)
            {
                var nazwaPliku = Path.GetFileName(uploadFile.FileName);
                var ścieżkaZapisu = Path.Combine(_environment.WebRootPath, "uploads", nazwaPliku);

                using (var strumień = new FileStream(ścieżkaZapisu, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(strumień);
                }
                course.PhotoPath = nazwaPliku;
            }
            _coursesService.NewCourse(course);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public RedirectToActionResult DeleteCourse(int id)
        {
            _coursesService.RemoveCourseById(id);

            return RedirectToAction("Index", "Courses");
        }

        public IActionResult GetUserCourses()
        {
            List<Course> courses = _coursesService.GetUserCourses(_userService.GetByEmail(User.Identity.Name).Id);

            return View(courses);
        }

        public List<User> GetCourseUsers(int courseId)
        {
            List<User> users = _coursesService.GetCourseUsers(courseId);

            return users;
        }
    }
}
