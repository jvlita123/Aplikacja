using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
            List<Reservation1> reservations = _reservation1Repository.GetAll().Include(x => x.Status).Include(x => x.User.Role).Include(x => x.User).Include(x => x.User.Photos).Include(x => x.Service).Include(x => x.ReservationSlot).ToList();

            return reservations;
        }

        public List<Reservation1> GetReservationsByStatus(string status)
        {
            List<Reservation1> reservations = _reservation1Repository.GetAll().Include(x => x.User).Include(x => x.Status).Include(x => x.User.Photos).Include(x => x.ReservationSlot).Where(x => x.Status.Name == status).ToList();
            return reservations;
        }

        public List<Reservation1> GetUserReservations(int id)
        {
            var usr = _userRepository.GetById(id);
            if (usr.Role.Name == "admin")
            {
                return _reservation1Repository.GetAll().ToList();
            }
            else
            {
                return _reservation1Repository.GetAll().ToList();
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
               UserPhotoPath = reservation.UserPhotoPath,
                Title = reservation.Title,
            };
            _reservation1Repository.AddAndSaveChanges(newReservation);
            _reservation1Repository.SaveChanges();

            ReservationSlots reservationSlot = _reservationSlotsRepository.GetById(newReservation.ReservationSlotId);
            reservationSlot.IsAvailable = false;
            reservationSlot.ReservationId = newReservation.Id;

            _reservationSlotsRepository.Update(reservationSlot);    
            _reservationSlotsRepository.SaveChanges();

            return newReservation;
        }

        public void RemoveReservation(int id)
        {
            var resSlotId = _reservation1Repository.GetById(id).ReservationSlotId;
            int slotId = _reservationSlotsRepository.GetAll().Where(x => x.Id == resSlotId).First().Id;

            _reservation1Repository.RemoveById(id);
            _reservation1Repository.SaveChanges();

            List<ReservationSlots> slots = _reservationSlotsRepository.GetAll().Where(x => x.Id == slotId).ToList();
            foreach ( ReservationSlots slot in slots)
            {
                slot.IsAvailable = true;
            }
            _reservationSlotsRepository.UpdateRangeAndSaveChanges(slots);

        }
        public Reservation1 UploadFile(string filePath, int id)
        {
            Reservation1 reservation = _reservation1Repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
            reservation.AdminPhotoPath = filePath;

            _reservation1Repository.Update(reservation);
            _reservation1Repository.SaveChanges();
            return reservation;
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
