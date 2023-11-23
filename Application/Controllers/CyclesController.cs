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

        public CyclesController(CyclesService cyclesService, ReservationService reservationService, CoursesService coursesService, ServiceService serviceService, StatusService statusService, UserService userService, EnrollmentsService enrollmentsService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
            _enrollmentService = enrollmentsService;
            _coursesService = coursesService;
            _cyclesService = cyclesService;
        }

        public ActionResult Index()
        {
            return View();
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
