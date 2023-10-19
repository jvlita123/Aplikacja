using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using System.Globalization;

namespace Application.Controllers
{
    public class ReservationController : Controller
    {
        private ReservationService _reservationService;
        private UserService _userService;
        private ServiceService _serviceService;
        private StatusService _statusService;

        public ReservationController(ReservationService reservationService, ServiceService serviceService,StatusService statusService,UserService userService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService; 
        }

        public IActionResult Index()
        {
            List<Reservation> reservations = _reservationService.GetAll().ToList();

            return View(reservations);
        }

        public IActionResult UserReservations()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            return View(_reservationService.getUserReservations(usr));
        }

        public IActionResult Calendar()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            ViewData["events"] = _reservationService.getUserReservations(usr);

            return View();
        }

        public IActionResult ReservationCalendar()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            var reservations = _reservationService.GetAll().Where(x => x.User.Role.Name == "admin").ToList();

            ViewData["events"] = reservations;

            return View();
        }

        [HttpGet]
        public IActionResult NewReservation()
        {
            var services = new SelectList(_serviceService.GetAll().Select(x => x.Name).ToList());
            var statuses = new SelectList(_statusService.GetAll().Select(x => x.Name).ToList());

            ViewBag.ServiceId = services;
            ViewBag.StatusId = statuses;

            return View();
        }

        [HttpPost]
        public IActionResult NewReservation(Reservation reservation)
        {
            reservation.UserId = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).Select(x=>x.Id).FirstOrDefault();

            _reservationService.newReservation(reservation);
            
            return View();
        }


        // POST: ReservationController/Create
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

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/5
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

        // GET: ReservationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationController/Delete/5
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
