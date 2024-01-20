using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class UserReservationSlotsService
    {
        private readonly UserReservationSlotsRepository _userReservationSlotsRepository;

        public UserReservationSlotsService(UserReservationSlotsRepository userReservationSlotsRepository)
        {
            _userReservationSlotsRepository = userReservationSlotsRepository;
        }

        public List<UserReservationSlots> GetAll()
        {
            List<UserReservationSlots> userReservationSlots = _userReservationSlotsRepository.GetAll().Include(x => x.User).Include(x => x.ReservationSlots).ToList();

            return userReservationSlots;
        }

        public UserReservationSlots AddNewUserReservationSlot(int userId, int reservationSlotId)
        {
            if (!(_userReservationSlotsRepository.GetAll().Where(x => x.UserId == userId && x.ReservationSlotsId == reservationSlotId).ToList().Count() > 0))
            {
                UserReservationSlots newUserReservationSlots = new()
                {
                    UserId = userId,
                    ReservationSlotsId = reservationSlotId,
                };
                _userReservationSlotsRepository.Add(newUserReservationSlots);
                _userReservationSlotsRepository.SaveChanges();
                return newUserReservationSlots;
            }
            return null;
        }
    }
}
