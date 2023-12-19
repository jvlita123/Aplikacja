using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class CyclesController : Controller
    {
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;
        private readonly CyclesService _cyclesService;
        private IWebHostEnvironment _environment;


        public CyclesController(IWebHostEnvironment environment,CyclesService cyclesService, ReservationService reservationService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
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
            return View(_cyclesService.GetAll());
        }
        public List<Cycle> GetCyclesForCourse(int courseId)
        {
            var cycles = _cyclesService.GetAll().Where(x => x.CourseId == courseId).ToList();
            return cycles;
        }

        public PartialViewResult GetCycle(int id)
        {
            return PartialView(_cyclesService.GetById(id));
        }

        [HttpGet]
        public PartialViewResult NewCycle(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }

        [HttpPost]
        public IActionResult NewCycle(Cycle cycle)
        {
            _cyclesService.NewCycle(cycle);

            return RedirectToAction("Index","Courses");
        }

        public IActionResult DeleteCycle(Cycle cycle)
        {
            return View(cycle);
        }

        [HttpPost]
        public RedirectToActionResult DeleteCycle(int cycleId)
        {
            _cyclesService.RemoveCycleById(cycleId);

            return RedirectToAction("Index", "Courses");
        }


        [HttpPost]
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

            return RedirectToAction("Index", "Courses");
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

        // GET: CyclesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CyclesController/Edit/5
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

        // GET: CyclesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CyclesController/Delete/5
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
