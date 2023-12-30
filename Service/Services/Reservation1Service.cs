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
    public class Reservation1Service
    {
        private readonly Reservation1Repository _reservation1Repository;
        private readonly ReservationSlotsRepository _reservationSlotsRepository;
        private readonly UserRepository _userRepository;
        private readonly StatusRepository _statusRepository;
        public Reservation1Service(Reservation1Repository reservation1Repository, UserRepository userRepository, StatusRepository statusRepository, ReservationSlotsRepository reservationSlotsRepository)
        {
            _reservation1Repository = reservation1Repository;
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _reservationSlotsRepository = reservationSlotsRepository;
        }

        public List<Reservation1> GetAll()
        {
            List<Reservation1> reservations = _reservation1Repository.GetAll()
                .Include(x => x.Status)
                .Include(x => x.User.Role)
                .Include(x => x.User.Reservations)
                .Include(x => x.ReservationSlot) // Dodaj Include dla ReservationSlot
                .Include(x => x.User)
                .ToList();

            return reservations;
        }

        public List<Reservation1> GetReservationsByStatus(string status)
        {
            List<Reservation1> reservations = _reservation1Repository.GetAll().Include(x => x.User).Include(x => x.Status).Include(x => x.User.Photos).Include(x => x.ReservationSlot).Where(x => x.Status.Name == status).ToList();
            return reservations;
        }

        public List<Reservation1> GetUserReservations(User usr)
        {
            if (usr.Role.Name == "admin")
            {
                return _reservation1Repository.GetAll().Include(x => x.Status).Include(x => x.Service).Include(x => x.ReservationSlot).ToList();
            }
            else
            {
                return _reservation1Repository.GetAll().Include(x => x.Status).Where(x => x.UserId == usr.Id).ToList();
            }
        }

        public Reservation1 NewReservation(Reservation1 reservation)
        {
            Reservation1 newReservation = new()
            {
                UserId = reservation.UserId,
                PrimaryColor = reservation.PrimaryColor,
                StatusId = reservation.StatusId,
                Status = reservation.Status,
                ServiceId = reservation.ServiceId,
                Service = reservation.Service,
                ReservationSlot = reservation.ReservationSlot,
                ReservationSlotId = reservation.ReservationSlotId,
                Title = reservation.Title,
            };
            ReservationSlots reservationSlot = _reservationSlotsRepository.GetById(reservation.ReservationSlotId);
            reservationSlot.IsAvailable = false;
            _reservationSlotsRepository.Update(reservationSlot);    
            _reservationSlotsRepository.SaveChanges();
            _reservation1Repository.AddAndSaveChanges(newReservation);

            return newReservation;
        }

        public void RemoveReservation(int id)
        {
            _reservation1Repository.RemoveById(id);
            _reservation1Repository.SaveChanges();
        }
        public void UpdateReservation(Reservation1 updatedReservation)
        {
            Reservation1 reservationToSave = _reservation1Repository.GetById(updatedReservation.Id);

            reservationToSave.Title = updatedReservation.Title;
            reservationToSave.PrimaryColor = updatedReservation.PrimaryColor;
            reservationToSave.StatusId = updatedReservation.StatusId;
            reservationToSave.Status = updatedReservation.Status;
            reservationToSave.Service = updatedReservation.Service;
            reservationToSave.ServiceId = updatedReservation.ServiceId;
            reservationToSave.ReservationSlot = updatedReservation.ReservationSlot;
            reservationToSave.ReservationSlotId = updatedReservation.ReservationSlotId;
            
            _reservation1Repository.Update(reservationToSave);

            _reservation1Repository.UpdateAndSaveChanges(reservationToSave);
            _reservation1Repository.SaveChanges();
        }
        public Reservation1 changeReservationStatus(int idReservation, int idStatus)
        {
            Reservation1 reservation = _reservation1Repository.GetById(idReservation);
            reservation.StatusId = idStatus;
            _reservation1Repository.UpdateAndSaveChanges(reservation);
            return reservation;
        }
    }
}
