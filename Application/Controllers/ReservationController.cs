using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class ReservationController : Controller
    {
        private ReservationService _reservationService;
        private UserService _userService;

        public ReservationController(ReservationService reservationService, UserService userService)
        {
            _reservationService = reservationService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            List<Reservation> reservations = _reservationService.GetAll().ToList();

            return View(reservations);
        }

        public IActionResult UserReservations()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            List<Reservation> reservations;

            if (User.IsInRole("admin"))
            {
                reservations = _reservationService.GetAll().ToList();
            }
            else
            {
                reservations = _reservationService.GetAll().Where(x => x.UserId == usr.Id).ToList();
            }

            return View(reservations);
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
