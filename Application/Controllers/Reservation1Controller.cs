using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;

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
        private readonly IWebHostEnvironment _environment;

        public Reservation1Controller(Reservation1Service reservation1Service, ServiceService serviceService, StatusService statusService, UserService userService, ReservationSlotsService reservationSlotsService, UserReservationSlotsService userReservationSlotsService, IWebHostEnvironment environment)
        {
            _reservation1Service = reservation1Service;
            _userService = userService;
            _serviceService = serviceService;
            _statusService = statusService;
            _reservationSlotsService = reservationSlotsService;
            _userReservationSlotsService = userReservationSlotsService;
            _environment = environment;
        }

        [Route("UserReservations")]
        [Authorize]
        public ActionResult Index()
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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User? usr = _userService.GetAll().Where(x => x.Email == HttpContext.User.Identity?.Name).FirstOrDefault();
                var userReservations = _reservation1Service.GetUserReservations(usr.Id);
                ViewData["userReservations"] = userReservations;

            }
            else
            {
                ViewData["userReservations"] = new List<Reservation1>();
            }
            var services = _serviceService.GetAll().ToList();
            var availableSlots = _reservationSlotsService.GetAll().Where(x => x.IsAvailable == true);
            var busySlots = _reservationSlotsService.GetAll().Where(x => x.IsAvailable == false);

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
        [Authorize]
        public PartialViewResult GetReservationDetails(int reservationId)
        {
            Reservation1 reservation = _reservation1Service.GetAll().Where(x => x.Id == reservationId).First();
            return PartialView(reservation);
        }

        [HttpGet]
        public PartialViewResult getReservationsByStatus(string status)
        {
            if (status == "All")
            {
                return PartialView(_reservation1Service.GetAll());
            }
            return PartialView(_reservation1Service.GetReservationsByStatus(status));
        }

        [HttpGet]
        [Authorize]
        public PartialViewResult NewReservation(int serviceId, DateTime selectedDate, int reservationSlotsId)
        {
            var emails = new SelectList(_userService.GetAll().Select(x => x.Email).ToList());
            var serviceName = _serviceService.GetAll().Where(x => x.Id == serviceId).Select(x => x.Name).FirstOrDefault();
            var reservationSlots = _reservationSlotsService.GetAll().Where(x => x.Id == reservationSlotsId).FirstOrDefault();

            ViewBag.emails = emails;
            ViewData["serviceName"] = serviceName;
            ViewData["selectedDate"] = selectedDate;
            ViewData["reservationSlots"] = reservationSlots;

            return PartialView();
        }

        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile uploadFile)
        {
            if (uploadFile != null && uploadFile.Length > 0)
            {
                var nazwaPliku = Path.GetFileName(uploadFile.FileName);
                var ścieżkaZapisu = Path.Combine(_environment.WebRootPath, "uploads", nazwaPliku);

                using (var strumień = new FileStream(ścieżkaZapisu, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(strumień);
                }
            }
            return RedirectToAction("Calendar");

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadFile1(IFormFile uploadFile, int id)
        {
            if (uploadFile != null && uploadFile.Length > 0)
            {
                var nazwaPliku = Path.GetFileName(uploadFile.FileName);
                var ścieżkaZapisu = Path.Combine(_environment.WebRootPath, "uploads", nazwaPliku);

                using (var strumień = new FileStream(ścieżkaZapisu, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(strumień);
                }

                var file = nazwaPliku;
                _reservation1Service.UploadFile(file, id);
            }
            return RedirectToAction("Index");

        }

        [HttpPost("NewReservation")]
        [Authorize]
        public async Task<IActionResult> NewReservation(Reservation1 reservation, string userEmail, IFormFile uploadFile)
        {
            await UploadFile(uploadFile);
            string nazwaPliku = Path.GetFileName(uploadFile.FileName);

            reservation.UserPhotoPath = nazwaPliku;
            reservation.UserId = _userService.GetAll().Where(x => x.Email == userEmail).Select(x => x.Id).FirstOrDefault();
            reservation.ServiceId = _serviceService.GetAll().Where(x => x.Name == reservation.Title).Select(x => x.Id).FirstOrDefault();
            reservation.StatusId = 4;

            var newReservation = _reservation1Service.NewReservation(reservation);

            return RedirectToAction("Calendar");
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveReservation(int id)
        {
            Reservation1 ReservationToRemove = _reservation1Service.GetAll().Where(x => x.Id == id).FirstOrDefault();
            _reservation1Service.RemoveReservation(id);

            return RedirectToAction("Calendar");
        }

        [HttpGet]
        [Authorize]
        public PartialViewResult changeReservationStatus(int id)
        {
            var StatusList = new SelectList(_statusService.GetAll().Select(x => x.Name).ToList());

            ViewBag.StatusList = StatusList;
            ViewBag.ReservationId = id;

            return PartialView();
        }

        [HttpPost]
        [Authorize]
        public IActionResult changeReservationStatus(int id, string status)
        {
            _reservation1Service.changeReservationStatus(id, _statusService.GetByName(status).Id);
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public IActionResult SubscribeSlot(int reservationSlotId)
        {
            var usr = _userService.GetByEmail(HttpContext.User.Identity.Name);
            UserReservationSlots userSlots = _userReservationSlotsService.AddNewUserReservationSlot(usr.Id, reservationSlotId);
            if (userSlots == null)
            {
                return null;
            }
            return NoContent();
        }
    }
}
