using Data.Dto_s;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;

namespace Application.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        public ReservationController(ReservationService reservationService, ServiceService serviceService, StatusService statusService, UserService userService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
        }

        public ActionResult Index()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            UserReservationsDto userReservationsDto = new();
            userReservationsDto.FinishedReservations = _reservationService.GetFinished(usr);
            userReservationsDto.ConfirmedReservations = _reservationService.GetConfirmed(usr);
            userReservationsDto.CancelledReservations = _reservationService.GetCancelled(usr);
            userReservationsDto.PendingReservations = _reservationService.GetPending(usr);

            return View(userReservationsDto);
        }

        public ActionResult ShowReservation(int id)
        {
            Reservation reservation = _reservationService.GetAll().Where(x => x.Id == id).First();

            return PartialView(reservation);
        }

        public IActionResult Calendar()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            var admin = _reservationService.GetAll().ToList();

            var services = _serviceService.GetAll().ToList();

            ViewData["events"] = _reservationService.GetUserReservations(usr);
            ViewData["adminEvents"] = admin;
            ViewData["services"] = services;

            return View();
        }

        [HttpGet]
        public PartialViewResult NewReservation(DateTime start, String serviceName)
        {
            var statuses = new SelectList(_statusService.GetAll().Select(x => x.Id).ToList());
            var services = new SelectList(_serviceService.GetAll().Select(x => x.Name).ToList());
            var emails = new SelectList(_userService.GetAll().Select(x => x.Email).ToList());

            ViewBag.Start = start.ToString("yyyy-MM-ddTHH:mm:ss");
            ViewBag.serviceName = serviceName;
            ViewBag.Emails = emails;
            ViewBag.services = services;
            ViewBag.StatusId = statuses;

            return PartialView();
        }

        [HttpPost]
        public IActionResult NewReservation(Reservation reservation, string userEmail)
        {
            reservation.UserId = _userService.GetAll().Where(x => x.Email == userEmail).Select(x => x.Id).FirstOrDefault();
            reservation.ServiceId = _serviceService.GetAll().Where(x => x.Name == reservation.Title).Select(x => x.Id).FirstOrDefault();

            var service = _serviceService.GetAll().Where(x => x.Name == reservation.Title).FirstOrDefault();

            var serviceDuration = service.ServiceTime;
            reservation.End = (DateTime)(reservation.Start + serviceDuration);

            reservation.StatusId = 4;
            _reservationService.NewReservation(reservation);

            return RedirectToAction("Calendar");
        }

        public IActionResult RemoveReservation(Reservation reservation)
        {
            Reservation ReservationToRemove = _reservationService.GetAll().Where(x => x.Id == reservation.Id).FirstOrDefault();
            return View(ReservationToRemove);
        }

        [HttpPost]
        public RedirectToActionResult RemoveReservation(int id)
        {
            Reservation ReservationToRemove = _reservationService.GetAll().Where(x => x.Id == id).FirstOrDefault();
            _reservationService.RemoveReservation(id);

            return RedirectToAction("Index");
        }

        public PartialViewResult EditReservation(int id)
        {
            Reservation reservation = _reservationService.GetAll().Where(x => x.Id == id).FirstOrDefault();

            var services = new SelectList(_serviceService.GetAll().Select(x => x.Id).ToList());
            var statuses = new SelectList(_statusService.GetAll().Select(x => x.Id).ToList());

            ViewBag.ServiceId = services;
            ViewBag.StatusId = statuses;

            return PartialView(reservation);
        }

        [HttpPost]
        public IActionResult EditReservation(Reservation reservation)
        {
            var services = new SelectList(_serviceService.GetAll().Select(x => x.Id).ToList());
            var statuses = new SelectList(_statusService.GetAll().Select(x => x.Id).ToList());

            ViewBag.ServiceId = services;
            ViewBag.StatusId = statuses;

            _reservationService.UpdateReservation(reservation);
            return RedirectToAction("Calendar");
        }

        public PartialViewResult changeReservationStatus(int id)
        {
            var StatusList = new SelectList(_statusService.GetAll().Select(x => x.Name).ToList());

            ViewBag.StatusList = StatusList;
            ViewBag.ReservationId = id;

            return PartialView();
        }

        [HttpPost]
        public IActionResult changeReservationStatus(int id, string status)
        {
            _reservationService.changeReservationStatus(id, _statusService.GetByName(status).Id);
            return RedirectToAction("Index");
        }
    }
}
