using Service.Entities;
using Service.Repositories;
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
        public ReservationService(ReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public List<Reservation> GetAll()
        {
            List<Reservation> reservations = _reservationRepository.GetAll().ToList();

            return reservations;
        }

        public Reservation Add(Reservation reservation)
        {
            Reservation newReservation = _reservationRepository.AddAndSaveChanges(reservation);

            return newReservation;
        }
    }
}
