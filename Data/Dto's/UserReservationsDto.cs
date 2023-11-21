using Data.Entities;

namespace Data.Dto_s
{
    public class UserReservationsDto
    {
        public List<Reservation>? ConfirmedReservations { get; set; }
        public List<Reservation>? FinishedReservations { get; set; }
        public List<Reservation>? CancelledReservations { get; set; }
        public List<Reservation>? PendingReservations { get; set; }
    }
}
