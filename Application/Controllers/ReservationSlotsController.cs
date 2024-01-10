using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using System.Collections.ObjectModel;

namespace Application.Controllers
{
    public class ReservationSlotsController : Controller
    {
        private readonly ReservationSlotsService _reservationSlotsService;
        private readonly ServiceService _serviceService;

        public ReservationSlotsController(ReservationSlotsService reservationSlotsService, ServiceService serviceService)
        {
            _reservationSlotsService = reservationSlotsService;
            _serviceService = serviceService;
        }


        [HttpGet]
        public PartialViewResult NewReservationSlot(DateTime selectedDate)
        {
            var services = new Collection<Data.Entities.Service>(_serviceService.GetAll().ToList());
            ViewData["services"] = services;
            ViewData["selectedDate"] = selectedDate;

            return PartialView();
        }

        [HttpPost]
        public IActionResult NewReservationSlot(ReservationSlots reservationSlot)
        {
            var newReservationSlot = _reservationSlotsService.NewReservationSlot(reservationSlot);
            if (newReservationSlot == null)
            {
                TempData["ErrorMessage"] = "Chosen date conflicts with another date. Please select a different time.";
                return RedirectToAction(nameof(SlotsCalendar));
            }
            return RedirectToAction(nameof(SlotsCalendar));
        }


        [HttpGet]
        public PartialViewResult GetSlotDetails(int slotId)
        {
            ReservationSlots slot = _reservationSlotsService.GetAll().Where(x => x.Id == slotId).First();
            return PartialView(slot);
        }

        public IActionResult GetReservationSlot(int id)
        {
            var reservationSlot = _reservationSlotsService.GetById(id);

            if (reservationSlot == null)
            {
                return NotFound();
            }

            return Ok(reservationSlot);
        }

        [HttpPost]
        public IActionResult RemoveReservationSlot(int slotId)
        {
            ReservationSlots ReservationSlotToRemove = _reservationSlotsService.GetAll().Where(x => x.Id == slotId).FirstOrDefault();
            _reservationSlotsService.RemoveReservationSlot(slotId);

            return NoContent();
        }

        public IActionResult SlotsCalendar()
        {
            var services = _serviceService.GetAll().ToList();
            var slots = _reservationSlotsService.GetAll().ToList();

            ViewData["slots"] = slots;
            ViewData["services"] = services;

            return View();
        }

    }
}
