using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class CyclesController : Controller
    {
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly CyclesService _cyclesService;
        private readonly IWebHostEnvironment _environment;

        public CyclesController(IWebHostEnvironment environment, CyclesService cyclesService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
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
            return View(_cyclesService.GetAll());
        }

        [Authorize]
        public List<Cycle> GetCyclesForCourse(int courseId)
        {
            var cycles = _cyclesService.GetAll().Where(x => x.CourseId == courseId).ToList();
            return cycles;
        }

        [Authorize]
        public PartialViewResult GetCycle(int id)
        {
            return PartialView(_cyclesService.GetById(id));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public PartialViewResult NewCycle(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult NewCycle(Cycle cycle)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(cycle);
            }

            _cyclesService.NewCycle(cycle);
            return RedirectToAction("Index", "Courses");
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteCycle(Cycle cycle)
        {
            return View(cycle);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public RedirectToActionResult DeleteCycle(int cycleId)
        {
            _cyclesService.RemoveCycleById(cycleId);

            return RedirectToAction("Index", "Courses");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UploadFile(IFormFile uploadFileCycle, int id)
        {
            if (uploadFileCycle != null && uploadFileCycle.Length > 0)
            {
                var nazwaPliku = Path.GetFileName(uploadFileCycle.FileName);
                var ścieżkaZapisu = Path.Combine(_environment.WebRootPath, "uploads", nazwaPliku);

                using (var strumień = new FileStream(ścieżkaZapisu, FileMode.Create))
                {
                    await uploadFileCycle.CopyToAsync(strumień);
                }

                var file = "uploads/" + nazwaPliku;
                _cyclesService.UploadFile(file, id);
            }
            return NoContent();
        }
    }
}
