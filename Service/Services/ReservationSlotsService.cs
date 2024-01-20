using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class ReservationSlotsService
    {
        private readonly ReservationSlotsRepository _reservationSlotsRepository;
        private readonly UserRepository _userRepository;
        private readonly StatusRepository _statusRepository;
        private readonly ReservationRepository _reservation1Repository;
        public ReservationSlotsService(UserRepository userRepository, StatusRepository statusRepository, ReservationSlotsRepository reservationSlotsRepository, ReservationRepository reservation1Repository)
        {
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _reservationSlotsRepository = reservationSlotsRepository;

            _reservation1Repository = reservation1Repository;

            var reservationSlots = _reservationSlotsRepository.GetAll().Include(x => x.UserReservationSlots).ToList();
        }

        public List<ReservationSlots> GetAll()
        {
            List<ReservationSlots> reservationSlots = _reservationSlotsRepository.GetAll().Include(x => x.Reservation).Include(x => x.Service).ToList();

            return reservationSlots;
        }
        public void RemoveReservationSlot(int id)
        {
            _reservation1Repository.RemoveByIdAndSaveChanges((int)_reservationSlotsRepository.GetById(id).ReservationId);
            _reservationSlotsRepository.RemoveByIdAndSaveChanges(id);
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
                    ReservationId = reservationSlots.ReservationId,
                    Date = reservationSlots.Date,
                    StartTime = reservationSlots.StartTime,
                    EndTime = reservationSlots.EndTime,
                    IsAvailable = true,
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
                    (
                        (newSlot.StartTime < existingSlot.StartTime && newSlot.EndTime >= existingSlot.EndTime) ||
                        (newSlot.StartTime < existingSlot.StartTime && newSlot.EndTime > existingSlot.EndTime) ||
                        (newSlot.StartTime < existingSlot.StartTime && newSlot.EndTime < existingSlot.EndTime) ||
                        (newSlot.StartTime >= existingSlot.StartTime && newSlot.EndTime <= existingSlot.EndTime) ||
                        (newSlot.StartTime >= existingSlot.StartTime && newSlot.StartTime < existingSlot.EndTime && newSlot.EndTime >= existingSlot.EndTime) 
                    )
                )
                .ToList();

            bool isSlotAvailable = !existingSlots.Any();

            return isSlotAvailable;
        }

    }
}
