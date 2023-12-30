using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReservationSlotsService
    {
        private readonly ReservationSlotsRepository _reservationSlotsRepository;
        private readonly UserRepository _userRepository;
        private readonly StatusRepository _statusRepository;
        public ReservationSlotsService(UserRepository userRepository, StatusRepository statusRepository, ReservationSlotsRepository reservationSlotsRepository)
        {
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _reservationSlotsRepository = reservationSlotsRepository;
        }

        public List<ReservationSlots> GetAll()
        {
            List<ReservationSlots> reservationSlots = _reservationSlotsRepository.GetAll().Include(x => x.Reservation).Include(x => x.Service).ToList();

            return reservationSlots;
        }

        public ReservationSlots GetById(int id)
        {
            ReservationSlots reservationSlot = _reservationSlotsRepository.GetAll().Include(x => x.Reservation).Include(x => x.Service).Where(x => x.Id == id).FirstOrDefault();

            return reservationSlot;
        }

        public ReservationSlots NewReservationSlot(ReservationSlots reservationSlots)
        {
            if (IsSlotAvailable(reservationSlots))
            {
                ReservationSlots newReservationSlots = new()
                {
                    ServiceId = reservationSlots.ServiceId,
                    Service = reservationSlots.Service,
                    ReservationId = reservationSlots.ReservationId,
                    Reservation = reservationSlots.Reservation,
                    Date = reservationSlots.Date,
                    StartTime = reservationSlots.StartTime,
                    EndTime = reservationSlots.EndTime,
                    IsAvailable = reservationSlots.IsAvailable,
                };

                _reservationSlotsRepository.AddAndSaveChanges(newReservationSlots);

                return newReservationSlots;
            }

            return null;
        }

        private bool IsSlotAvailable(ReservationSlots newSlot)
        {
            var existingSlots = _reservationSlotsRepository
                .GetAll()
                .Where(existingSlot =>
                    existingSlot.Date == newSlot.Date &&
                    existingSlot.Id != newSlot.Id && 
                    newSlot.StartTime < existingSlot.EndTime && newSlot.EndTime > existingSlot.StartTime)
                .ToList();

            bool isSlotAvailable = !existingSlots.Any();

            return isSlotAvailable; 
        }

        public void RemoveReservationSlot(int id)
        {
            _reservationSlotsRepository.RemoveById(id);
            _reservationSlotsRepository.SaveChanges();
        }

    }
}
