using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using System.Security.Claims;

namespace Application.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;

        public EnrollmentController(CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
            _enrollmentService = enrollmentsService;
            _coursesService = coursesService;
        }

        public ActionResult Index()
        {
            List<Enrollment> enrollments = _enrollmentService.GetUserEnrollments(_userService.GetByEmail(HttpContext.User.Identity.Name));
            return View(enrollments);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult NewEnrollment(int id)
        {
            var emails = new SelectList(_userService.GetAll().Select(x => x.Email).ToList());
            var course = _coursesService.GetAll().Where(x => x.Id == id).FirstOrDefault();

            ViewBag.Emails = emails;
            ViewBag.course = course.Id;

            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewEnrollment(int courseId, string userEmail)
        {
            User user = _userService.GetAll().Where(x => x.Email == userEmail).FirstOrDefault();
            if (_enrollmentService.IsEnrolled(user.Id, courseId) == false)
            {
                Enrollment enrollment = new Enrollment();
                enrollment.UserId = _userService.GetAll().Where(x => x.Email == userEmail).Select(x => x.Id).FirstOrDefault();
                enrollment.CourseId = _coursesService.GetAll().Where(x => x.Id == courseId).Select(x => x.Id).FirstOrDefault();
                enrollment.EnrollmentDate = DateTime.Now;
                enrollment.Cancelled = false;

                _enrollmentService.NewEnrollment(enrollment);
                return RedirectToAction("Index", "Courses");
            }
            TempData["Message"] = "You are already enrolled in this course.";

            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "admin")
            {
                TempData["Message"] = "This user is already enrolled in this course.";
            }
            return RedirectToAction("Index", "Courses");

        }
    }
}
