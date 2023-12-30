using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Service.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Controllers
{
    public class Reservation1Controller : Controller
    {
        private readonly Reservation1Service _reservation1Service;
        private readonly UserService _userService;
        private readonly ServiceService _serviceService;
        private readonly StatusService _statusService;
        private readonly ReservationSlotsService _reservationSlotsService;
        private readonly UserReservationSlotsService _userReservationSlotsService;

        public Reservation1Controller(Reservation1Service reservation1Service, ServiceService serviceService, StatusService statusService, UserService userService, ReservationSlotsService reservationSlotsService, UserReservationSlotsService userReservationSlotsService)
        {
            _reservation1Service = reservation1Service;
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
            _reservationSlotsService = reservationSlotsService;
            _userReservationSlotsService = userReservationSlotsService;
        }

        public IActionResult Index()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            var statuses = _statusService.GetAll().ToList();
            ViewData["statuses"] = statuses;

            return View(_reservation1Service.GetUserReservations(usr.Id));
        }

        public IActionResult ShowReservation(int id)
        {
            Reservation1 reservation = _reservation1Service.GetAll().Where(x => x.Id == id).First();

            return PartialView(reservation);
        }

        public IActionResult Calendar()
        {
            var usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            var services = _serviceService.GetAll().ToList();
            var availableSlots = _reservationSlotsService.GetAll().Where(x => x.IsAvailable == true);
            var busySlots = _reservationSlotsService.GetAll().Where(x => x.IsAvailable == false);
            int id = usr.Id;
            var userReservations = _reservation1Service.GetUserReservations(id);
            foreach(var v in userReservations)
            {
                v.ReservationSlot = _reservationSlotsService.GetById(v.ReservationSlotId);
            }
            ViewData["userReservations"] = userReservations;
            ViewData["services"] = services;
            ViewData["availableSlots"] = availableSlots;
            ViewData["busySlots"] = busySlots;

            return View();
        }

        [HttpGet]
        public IActionResult GetSlots(int serviceId, DateTime selectedDate)
        {
            var availableSlots = _reservationSlotsService.GetAll()
                .Where(slot => slot.ServiceId == serviceId && slot.IsAvailable && slot.Date.Date == selectedDate)
                .ToList();

            var busySlots = _reservationSlotsService.GetAll()
                .Where(slot => slot.ServiceId == serviceId && !slot.IsAvailable && slot.Date.Date == selectedDate)
                .ToList();


            return Json(new { AvailableSlots = availableSlots, BusySlots = busySlots });
        }
                
        [HttpGet]
        public PartialViewResult GetReservationDetails(int reservationId)
        {
            Reservation1 reservation = _reservation1Service.GetAll().Where(x => x.Id == reservationId).First();
            return PartialView(reservation);
        }


        [HttpGet]
        public PartialViewResult NewReservation(int serviceId, DateTime selectedDate, int reservationSlotsId)
        {
            var emails = new SelectList(_userService.GetAll().Select(x => x.Email).ToList());
            var serviceName = _serviceService.GetAll().Where(x => x.Id == serviceId).Select(x => x.Name).FirstOrDefault();
            var reservationSlots = _reservationSlotsService.GetAll().Where(x => x.Id == reservationSlotsId).FirstOrDefault();

            ViewData["serviceName"] = serviceName;
            ViewBag.emails = emails;
            ViewData["selectedDate"] = selectedDate;
            ViewData["reservationSlots"] = reservationSlots;

            return PartialView();
        }

        [HttpPost]
        public IActionResult NewReservation(Reservation1 reservation, string userEmail)
        {
            reservation.UserId = _userService.GetAll().Where(x => x.Email == userEmail).Select(x => x.Id).FirstOrDefault();
            reservation.ServiceId = _serviceService.GetAll().Where(x => x.Name == reservation.Title).Select(x => x.Id).FirstOrDefault();

            var service = _serviceService.GetAll().Where(x => x.Name == reservation.Title).FirstOrDefault();

            var serviceDuration = service.ServiceTime;
            reservation.StatusId = 4;

            _reservation1Service.NewReservation(reservation);

            return RedirectToAction("Calendar");
        }

/*        public IActionResult RemoveReservation(Reservation1 reservation)
        {
            Reservation1 ReservationToRemove = _reservation1Service.GetAll().Where(x => x.Id == reservation.Id).FirstOrDefault();
            return View(ReservationToRemove);
        }*/

        [HttpPost]
        public IActionResult RemoveReservation(int id)
        {
            Reservation1 ReservationToRemove = _reservation1Service.GetAll().Where(x => x.Id == id).FirstOrDefault();
            _reservation1Service.RemoveReservation(id);

            return NoContent();
        }

        [HttpGet]
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
            _reservation1Service.changeReservationStatus(id, _statusService.GetByName(status).Id);
            return NoContent();
        }
        public PartialViewResult EditReservation(int id)
        {
            Reservation1 reservation = _reservation1Service.GetAll().Where(x => x.Id == id).FirstOrDefault();

            var statuses = new SelectList(_statusService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList(), "Value", "Text");

            var users = new SelectList(_userService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Email
            }).ToList(), "Value", "Text");


            ViewBag.Statuses = statuses;
            ViewBag.Users = users;

            return PartialView(reservation);
        }

        [HttpPost]
        public IActionResult EditReservation(Reservation1 reservation)
        {
            _reservation1Service.UpdateReservation(reservation);
            return NoContent();
        }

        [HttpPost]
        public IActionResult SubscribeSlot(int reservationSlotId)
        {
            var usr = _userService.GetByEmail(HttpContext.User.Identity.Name);
            UserReservationSlots userSlots = _userReservationSlotsService.AddNewUserReservationSlot(usr.Id, reservationSlotId);
            usr.UserReservationSlots.Add(userSlots);
            _reservationSlotsService.GetById(reservationSlotId).Attach(usr);
            return NoContent();
        }

        [HttpGet]
        public PartialViewResult getReservationsByStatus(string status)
        {
            if (status == "All")
            {
                var reservations = _reservation1Service.GetAll();
                return PartialView(reservations);
            }
            return PartialView(_reservation1Service.GetReservationsByStatus(status));
        }


        /*
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
                return View();
            }

            [HttpGet]
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
                return NoContent();
            }

            [HttpGet]
            public PartialViewResult getReservationsByStatus(string status)
            {
                if (status == "All")
                {
                    return PartialView(_reservationService.GetAll());
                }
                return PartialView(_reservationService.GetReservationsByStatus(status));
            }*/
    }
}
