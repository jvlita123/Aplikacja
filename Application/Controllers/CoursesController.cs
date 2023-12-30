using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Services;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Application.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly CyclesService _cyclesService;
        private IWebHostEnvironment _environment;


        public CoursesController(IWebHostEnvironment environment,CyclesService cyclesService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
            _enrollmentService = enrollmentsService;
            _coursesService = coursesService;
            _cyclesService = cyclesService;
            _environment = environment;
        }

        public ActionResult Index()
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
        public IActionResult NewCourse(Course course)
        {
            _coursesService.NewCourse(course);

            return RedirectToAction("Index");
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

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
