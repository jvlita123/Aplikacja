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
        public ReservationSlotsService(UserRepository userRepository, StatusRepository statusRepository, ReservationSlotsRepository reservationSlotsRepository)
        {
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _reservationSlotsRepository = reservationSlotsRepository;

            var reservationSlots = _reservationSlotsRepository.GetAll().Include(x => x.UserReservationSlots).ToList();

            foreach (var observer in reservationSlots)
            {
                foreach (var v in observer.UserReservationSlots)
                {
                    var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == v.UserId);
                    if (user != null && user.UserReservationSlots.Where(x => x.Id == observer.Id).ToList().Count() > 0)
                    {
                        observer.Attach(user);
                    }
                }
            }
        }

        public List<ReservationSlots> GetAll()
        {
            List<ReservationSlots> reservationSlots = _reservationSlotsRepository.GetAll().Include(x => x.Reservation).Include(x => x.Service).ToList();

            return reservationSlots;
        }
        public void RemoveReservationSlot(int id)
        {
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
                newSlot.StartTime < existingSlot.StartTime && newSlot.EndTime <= existingSlot.StartTime &&
                newSlot.StartTime >= existingSlot.EndTime && newSlot.EndTime > existingSlot.EndTime)
            .ToList();

            bool isSlotAvailable = !existingSlots.Any();

            return isSlotAvailable;
        }

    }
}
