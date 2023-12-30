using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            UserReservationSlots newUserReservationSlots = new()
            {
                UserId = userId,
                ReservationSlotsId = reservationSlotId,
            };
            _userReservationSlotsRepository.Add(newUserReservationSlots);
            _userReservationSlotsRepository.SaveChanges();
            return newUserReservationSlots;

        }
    }
}
