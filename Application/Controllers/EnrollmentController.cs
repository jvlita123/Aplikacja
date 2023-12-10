using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;

namespace Application.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;

        public EnrollmentController(ReservationService reservationService,CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _reservationService = reservationService;
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

        [HttpGet]
        public PartialViewResult NewEnrollment(int id)
        {
            var emails = new SelectList(_userService.GetAll().Select(x => x.Email).ToList());
            var course = _coursesService.GetAll().Where(x => x.Id == id).FirstOrDefault();

            ViewBag.Emails = emails;
            ViewBag.course = course.Id;

            return PartialView();
        }

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
                return RedirectToAction("GetCourse", "Courses", new { id = courseId });
            }

            return View();

        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
