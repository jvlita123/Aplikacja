/*using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly UserRepository _userRepository;
        private readonly StatusRepository _statusRepository;
        public ReservationService(ReservationRepository reservationRepository, UserRepository userRepository, StatusRepository statusRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _statusRepository = statusRepository;
        }

        public List<Reservation> GetAll()
        {
            List<Reservation> reservations = _reservationRepository.GetAll().Include(x => x.Status).Include(x => x.User.Role).Include(x => x.User).Include(x => x.Service)ToList();

            return reservations;
        }

        public List<Reservation> GetReservationsByStatus(string status)
        {
            List<Reservation> reservations = _reservationRepository.GetAll().Include(x => x.User).Include(x => x.Status).Include(x => x.User.Photos).Where(x => x.Status.Name == status).ToList();
            return reservations;
        }

        public List<Reservation> GetConfirmed(User usr)
        {
            List<Reservation> allUserReservations = GetUserReservations(usr);
            var confirmed = allUserReservations.Where(x => x.Status != null && x.Status.Name == "Confirmed").ToList();
            return confirmed;
        }

        public List<Reservation> GetFinished(User usr)
        {
            List<Reservation> allUserReservations = GetUserReservations(usr);
            List<Reservation> finished = allUserReservations.Where(x => x.Status != null && x.Status.Name == "Finished").ToList();
            return finished;
        }

        public List<Reservation> GetPending(User usr)
        {
            List<Reservation> allUserReservations = GetUserReservations(usr);
            List<Reservation> finished = allUserReservations.Where(x => x.Status != null && x.Status.Name == "Pending").ToList();

            return finished;
        }

        public List<Reservation> GetCancelled(User usr)
        {
            List<Reservation> allUserReservations = GetUserReservations(usr);
            List<Reservation> cancelled = allUserReservations.Where(x => x.Status != null && x.Status.Name == "Cancelled").ToList();

            return cancelled;
        }

        public List<Reservation> GetUserReservations(User usr)
        {
            if (usr.Role.Name == "admin")
            {
                return _reservationRepository.GetAll().Include(x => x.Status).ToList();
            }
            else
            {
                return _reservationRepository.GetAll().Include(x => x.Status).Where(x => x.UserId == usr.Id).ToList();
            }
        }

        public Reservation NewReservation(Reservation reservation)
        {
            Reservation newReservation = new()
            {
                UserId = reservation.UserId,
                Start = reservation.Start,
                End = reservation.End,
                SecondaryColor = reservation.SecondaryColor,
                PrimaryColor = reservation.PrimaryColor,
                StatusId = reservation.StatusId,
                Status = reservation.Status,
                ServiceId = reservation.ServiceId,
                Service = reservation.Service,
                Title = reservation.Title,
            };

            _reservationRepository.AddAndSaveChanges(newReservation);

            return reservation;
        }
        public void RemoveReservation(int id)
        {
            _reservationRepository.RemoveById(id);
            _reservationRepository.SaveChanges();
        }
        public void UpdateReservation(Reservation updatedReservation)
        {
            Reservation reservationToSave = _reservationRepository.GetById(updatedReservation.Id);

            reservationToSave.Start = updatedReservation.Start;
            reservationToSave.End = updatedReservation.End;
            reservationToSave.Title = updatedReservation.Title;
            reservationToSave.PrimaryColor = updatedReservation.PrimaryColor;
            reservationToSave.SecondaryColor = updatedReservation.SecondaryColor;
            reservationToSave.StatusId = updatedReservation.StatusId;
            reservationToSave.Status = updatedReservation.Status;
            reservationToSave.Service = updatedReservation.Service;
            reservationToSave.ServiceId = updatedReservation.ServiceId;
            _reservationRepository.Update(reservationToSave);

            _reservationRepository.UpdateAndSaveChanges(reservationToSave);
            _reservationRepository.SaveChanges();
        }
        public Reservation changeReservationStatus(int idReservation, int idStatus)
        {
            Reservation reservation = _reservationRepository.GetById(idReservation);
            reservation.StatusId = idStatus;
            _reservationRepository.UpdateAndSaveChanges(reservation);
            return reservation;
        }
    }
}
*/