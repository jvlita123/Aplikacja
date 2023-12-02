using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Services;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Hosting;

namespace Application.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly CyclesService _cyclesService;
        private IWebHostEnvironment _environment;


        public CoursesController(IWebHostEnvironment environment,CyclesService cyclesService, ReservationService reservationService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _reservationService = reservationService;
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
            ViewData["cycles"] = course.Cycles;
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

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: CoursesController/Create
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

        // GET: CoursesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoursesController/Edit/5
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

        // GET: CoursesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoursesController/Delete/5
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
