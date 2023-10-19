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
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly UserRepository _userRepository;
        public ReservationService(ReservationRepository reservationRepository, UserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
        }

        public List<Reservation> GetAll()
       {
            List<Reservation> reservations = _reservationRepository.GetAll().Include(x => x.User.Role).Include(x => x.User).ToList();

            return reservations;
        }

        public Reservation Add(Reservation reservation)
        {
            Reservation newReservation = _reservationRepository.AddAndSaveChanges(reservation);

            return newReservation;
        }

        public List<Reservation> getUserReservations(User usr)
        {
            if (usr.Role.Name == "admin")
            {
                return _reservationRepository.GetAll().ToList();
            }
            else
            {
                return _reservationRepository.GetAll().Where(x => x.UserId == usr.Id).ToList();
            }
        }

        public Reservation newReservation(Reservation reservation)
        {
            Reservation newReservation = new Reservation()
            {
                UserId = reservation.UserId,
                Start = reservation.Start,
                End = reservation.End,
                SecondaryColor = reservation.SecondaryColor,
                PrimaryColor = reservation.PrimaryColor,
                Status = reservation.Status,
                Service = reservation.Service,
                Title = reservation.Title,
            };

            _reservationRepository.AddAndSaveChanges(newReservation);
            
            return reservation;
        }
    }
}
