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
                TempData["ErrorMessage"] = "Wybrany slot koliduje z innym slotem. Wybierz inny czas.";
                return RedirectToAction(nameof(SlotsCalendar));
            }
            return CreatedAtAction(nameof(GetReservationSlot), new { id = newReservationSlot.Id }, newReservationSlot);
        }


        [HttpGet("{id}")]
        public IActionResult GetReservationSlot(int id)
        {
            var reservationSlot = _reservationSlotsService.GetById(id);

            if (reservationSlot == null)
            {
                return NotFound();
            }

            return Ok(reservationSlot);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservationSlot(int id)
        {
            try
            {
                _reservationSlotsService.RemoveReservationSlot(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to delete reservation slot: {ex.Message}");
            }
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
